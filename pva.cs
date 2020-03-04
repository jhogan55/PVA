using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Validator_Class;

namespace PVAfront
{
    public partial class pva : Form
    {
        public pva()
        {
            InitializeComponent();
        }

        //form level variables
        int countID = 1; //counter for individual IDs, will be used to link infant w/ mom 
        int ageClass; //3 age classes currently in play: 0 is infant, 1 is juve, 2 is adult 
        int age; //age in months 
        int infAge; //age of a dependent 
        List<simpleInd> startingPop = new List<simpleInd>(); //keep track of your starting population to reset between trials 
 
       

        //Instantiate objects of your other classes so you can use their methods. I think there's a better way to do this, but this works for now :) 
        Calc calc = new Calc(); //create a calc object
        VitalRates vr = new VitalRates(); //create a vital rates object
        simpleInd indMethods = new simpleInd(); //methods access 
       
        //Add button is clicked, user wants to add a monkey to starting population 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Step 1: retrieve data from form 
            if (Validator.TextEntered(txtAge, "Age")
                && Validator.IsInt(txtAge, "Age")
                && Validator.WithinRange(txtAge, 0, 360, "Age")) 
            {
                age = Convert.ToInt32(txtAge.Text); //get individual's age from form 
                ageClass = indMethods.AssignAgeClass(age); //get an age class designation based on age 
                simpleInd newInd = new simpleInd(countID, ageClass, age, false, 0, false, 0, 0); //create a new individual w no dependencies 
                countID++;

                //Does an adult female have a dependent infant? If yes, get age and sex of infant
                //REFACTOR THIS INTO OWN METHOD 
                if (chkDepInf.Checked)
                {
                    if (Validator.TextEntered(txtInfAge, "Infant Age")
                         && Validator.IsInt(txtInfAge, "Infant Age")
                        && Validator.WithinRange(txtInfAge, 0, 11, "Infant Age"))
                    {
                        infAge = Convert.ToInt32(txtInfAge.Text); //how old is dependent infant? 
                        newInd.DepInf = true; //declare infant dependency 
                        newInd.DepInfDuration = infAge + 6; //add 6 months of pregnancy time to age  

                        if (rdoFemale.Checked) //dependent infant is a female, need to add a baby to population
                        {
                            simpleInd baby = new simpleInd(countID, 0, infAge, false, 0, false, 0, newInd.IndID); //create baby, link to mom
                            countID++;
                            newInd.DepInfFem = true;
                            newInd.DepInfID = baby.IndID; //link mom to baby 
                            startingPop.Add(newInd);
                            startingPop.Add(baby);
                        }
                        else //male baby, need to track but not add to population 
                        {
                            newInd.DepInf = true;
                            startingPop.Add(newInd); //mom is added with 
                        }
                    }                  
                }

                else startingPop.Add(newInd); //no dependent infant info added, just add individual to population as is. 
                RefreshPopulation(startingPop);
                ResetForm();
            }
           
        }

        //Start simulation 
        private void btnRun_Click(object sender, EventArgs e)
        {
            //grab years and trials
            if (Validator.TextEntered(txtYears, "Years") && Validator.TextEntered(txtTrials, "Trials") 
                && Validator.IsInt(txtYears, "Years") && Validator.TextEntered(txtTrials, "Trials") 
                && Validator.WithinRange(txtYears,0, 1000, "Years") && Validator.WithinRange(txtTrials, 0, 100000, "Trials"))
            {               
                int years = Convert.ToInt32(txtYears.Text);
                int trials = Convert.ToInt32(txtTrials.Text);
                int months = years * 12;               
                RunSim(startingPop, months, trials);
                //countID = 0;
                ResetForm();
            }
           
        }

        //function: run sim
        private void RunSim(List<simpleInd> p, int m, int t)
        {
            List<Trial> trialList = new List<Trial>();
            
            //counters used at end of sim to display crude results 
            int biggerPops = 0; 
            int extinctPops = 0;
            int smallerPops = 0;
            int neutralPops = 0; 
            
            //Trial loop: run for the number of trials 
            for (int i = 0; i < t; i++)
            {              
                Trial trial = new Trial(); //new trial has started, add monthly results to this 
                List<simpleInd> trialPop = new List<simpleInd>(p); //population to change within trial
                lblRunning.Text = "Conducting trial " + i.ToString() + " of " + t;
                lblRunning.Invalidate();
                lblRunning.Update();
                //Risk related variables
                int riskLength = VitalRates.RISKPERIOD; //counter for AMRs, assumes month 0 of sim was NOT a takeover month so set counter above threshold 
                bool amrPeriod = false; //start of sim, default is no risk 
                
                //Time loop: run until you reach the # months 
                for (int j = 0; j < m; j++)
                {
                    //Step 1: age everyone up a month and add a month to necessary counters 
                    AgeUp(trialPop); //add a month to everyone's age 
                    trial.MonthOfTrial = j + 1; //this is the month within the trial 

                    //Step 2: Decide if takeover occurred and if you're still under risk period 
                    bool takeover = calc.CoinFlip(VitalRates.AMRRATE); //weighted coin flip for a new takeover 
                    
                    if (takeover) //takeover has occurred 
                    {
                        riskLength = 0; //reset the risk period length to 0
                        amrPeriod = true; //you are in a risk period 
                    }

                    else riskLength++; //no new takeover but youre in a risk period already, so add a month to risk counter
                    if (riskLength >= VitalRates.RISKPERIOD) amrPeriod = false; //if your risk period counter hits the threshold, turn off the risk variable                                           
                    Month newMonth = new Month(trialPop.Count(), amrPeriod, takeover); //create a month to keep track of variables 
                    
                    //Step 3: flip the weighted coin of death for each ind in your pop
                    //EVENTUALLY REFACTOR... SEPARATE METHOD AND MOVE TO NEW CLASS? 
                    for (int k = trialPop.Count -1 ; k >= 0; k--)
                    {
                        double rate = calc.SampleBeta(vr.ReturnSurvMean(trialPop[k].AgeClass, amrPeriod), vr.ReturnSurvSd(trialPop[k].AgeClass, amrPeriod));
                        bool surv = calc.CoinFlip(rate);
                        if (surv == false) //individual died
                        {                            
                            //if ind is adult female with dependent infants remove baby too
                            if (trialPop[k].DepInfID > 0) //if mom has a baby associated 
                            {
                                int babyToKill = trialPop[k].DepInfID; //just keep this until you escape the loop 
                                trialPop.RemoveAll(x => x.IndID == babyToKill);
                                newMonth.Deaths++;
                            } //getting baby ID from a dead mom 

                            //if ind is a dependent infant clear blocks from mom 
                            foreach (simpleInd mom in trialPop)
                            {
                                if (mom.IndID == trialPop[k].MomID)
                                {
                                   mom.DepInf = false;
                                   mom.DepInfID = 0;
                                   mom.DepInfFem = false;
                                   mom.DepInfDuration = 0;
                                } //mom alterations 

                            } //search population for the mom in question  

                            trialPop.RemoveAt(k); //remove ind from population 
                            newMonth.Deaths++;
                        } //death loop 
                    }

                    //Step 4: flip the death coin for infant males (only to release blocks from moms) 
                    foreach (simpleInd maleMoms in trialPop)
                    {
                        if (maleMoms.DepInf & maleMoms.DepInfFem == false)
                        {
                            double rate = calc.SampleBeta(vr.ReturnSurvMean(0, amrPeriod), vr.ReturnSurvSd(0, amrPeriod));
                            bool surv = calc.CoinFlip(rate);
                            if (!surv) //baby died, clear blocks from mom 
                            {
                                maleMoms.DepInf = false;
                                maleMoms.DepInfDuration = 0;
                            }
                        }
                    }

                    //Step 5: for surviving AFs, flip the baby coin
                    //EVENTUALLY REFACTOR... SEPARATE METHOD AND MOVE TO NEW CLASS? 
                    for (int l = trialPop.Count -1; l >= 0; l--)
                    {
                        if (trialPop[l].AgeClass == 2 & trialPop[l].DepInf == false) //if ind is an adult and not already with dependent infant they can try for a baby
                        {
                            bool newDep = calc.CoinFlip(calc.SampleBeta(vr.ReturnReprodMean(trialPop[l]), vr.ReturnReprodSd(trialPop[l]))); //weighted coin flip for baby or no 
                            if (newDep) //mom has a new baby this month 
                            {

                                trialPop[l].DepInf = true; 
                                trialPop[l].DepInfFem = calc.CoinFlip(VitalRates.SEXRATIO); //determine sex of baby 
                                
                                //It's a girl, so add to population 
                                if (trialPop[l].DepInfFem)
                                {
                                    simpleInd baby = new simpleInd(countID, 0, 0, false, 0, false, 0, trialPop[l].IndID);
                                    trialPop.Add(baby);
                                    trialPop[l].DepInfID = baby.IndID;
                                    countID++;
                                    newMonth.Births++;
                                } //female loop

                            } //new baby loop
                        } //reproduction opportunity 
                    }//baby attempt loop 
                    newMonth.PopEnd = trialPop.Count();
                    trial.MonthResults = newMonth;
                                     
                }//End of single trial
                //MessageBox.Show("End of trial " + Convert.ToString(i+1) + ". You simulated a starting population of " + p.Count() + " for " +  trial.MonthOfTrial.ToString() + " months, and ended with a population of " + trial.MonthResults.PopEnd.ToString());
                trialList.Add(trial);
            }//End of all trials
            
            //pull summary statistics from simulation 
            foreach (Trial trial in trialList)
            {
                if (trial.MonthResults.PopEnd == 0) { extinctPops++; }
                else if (trial.MonthResults.PopEnd > trial.MonthResults.PopStart + VitalRates.NEUTRALGROWTH) { biggerPops++; }
                else if (trial.MonthResults.PopEnd < trial.MonthResults.PopStart - VitalRates.NEUTRALGROWTH) { smallerPops++; }
                else neutralPops++;
            }

            MessageBox.Show("In total you conducted " + trialList.Count() + " trials of a starting population of " + p.Count() + ". The population increased in " + biggerPops + " trials, stayed the same in "
                + neutralPops + ", decreased in " + smallerPops + ", and went extinct in " + extinctPops);

            lblRunning.Text = "";
            lblRunning.Invalidate();
            lblRunning.Update();

        }//Full function 

        private void RefreshPopulation(List<simpleInd> p)
        {
            lstPop.Items.Clear();
            foreach (simpleInd i in p)
            {
                lstPop.Items.Add(i);
                txtStartPop.Text = Convert.ToString(lstPop.Items.Count);
            }
        }

        //Age up each ind after every month, verify age classes, click up the counters 
        private void AgeUp(List<simpleInd> p)
        {
            foreach (simpleInd ind in p)
            {
                ind.Age++;
                ind.AgeClass = ind.AssignAgeClass(ind.Age);
                if (ind.DepInf) { ind.DepInfDuration++; } //if you have a dependent infant add a month to their dependency counter 
                if (ind.DepInfDuration >= VitalRates.DEPENDENCY) //infant is now independent, break links 
                {
                    ind.DepInf = false; //mom loses dependency block
                    ind.DepInfDuration = 0; //infant dependency counter reset
                    ind.DepInfID = 0; //wipe dependent info data from mom
                }
                if (ind.Age >= VitalRates.JUVEAGE) ind.MomID = 0; //simple way to break the mom dependency from an infant who graduates 
            }
        }

        private void chkDepInf_CheckedChanged(object sender, EventArgs e)
        {
            if (Validator.TextEntered(txtAge, "Age")
                && Validator.IsInt(txtAge, "Age")
                && Validator.WithinRange(txtAge, 0, 360, "Age"))
            {
                int ageCheck = Convert.ToInt32(txtAge.Text);
                if (chkDepInf.Checked)
                {
                    if (ageCheck >= VitalRates.ADULTAGE) { grpInf.Enabled = true; }
                    else
                    {
                        MessageBox.Show("Individual is not old enough to have a dependent infant, must be an adult", "Error: Not of age for dependent");
                        chkDepInf.Checked = false;
                        txtAge.Focus();
                    }
                }
                else grpInf.Enabled = false;
            }         
        }

        private void ResetForm()
        {
            grpInf.Enabled = false;
            chkDepInf.Checked = false;
            txtAge.Clear();
            txtInfAge.Clear();
            txtAge.Focus();
            btnDefaultStart.Enabled = true;
        }

        //User wants to just use the default starting population for capuchins instead of a custom list 
        //Simulates LV as of March 2020
        //this is an awful setup but it works for now 
        private void btnDefaultStart_Click(object sender, EventArgs e)
        {

            startingPop.Clear();

            simpleInd sals = new simpleInd(1, 2, 296, true, 0, false, 17, 0); //male dep inf
            simpleInd chut = new simpleInd(2, 2, 253, false, 0, false, 0, 0);
            simpleInd oreg = new simpleInd(3, 2, 179, true, 0, false, 16, 0); // male dep inf 
            simpleInd chch = new simpleInd(4, 2, 190, false, 0, false, 0, 0);
            simpleInd thym = new simpleInd(5, 2, 147, false, 0, false, 0, 0);
            simpleInd vani = new simpleInd(6, 2, 123, false, 0, false, 0, 0);
            simpleInd sage = new simpleInd(7, 2, 121, false, 0, false, 0, 0);
            simpleInd crys = new simpleInd(8, 2, 93, false, 0, false, 0, 0);
            simpleInd roux = new simpleInd(9, 1, 72, false, 0, false, 0, 0);
            simpleInd fres = new simpleInd(10, 1, 72, false, 0, false, 0, 0);
            simpleInd papr = new simpleInd(11, 1, 37, false, 0, false, 0, 0);
            simpleInd hari = new simpleInd(12, 1, 19, false, 0, false, 0, 0);

            startingPop.Add(sals);
            startingPop.Add(chut);
            startingPop.Add(oreg);
            startingPop.Add(chch);
            startingPop.Add(thym);
            startingPop.Add(vani);
            startingPop.Add(sage);
            startingPop.Add(crys);
            startingPop.Add(roux);
            startingPop.Add(fres);
            startingPop.Add(papr);
            startingPop.Add(hari);

            countID = 13;
            RefreshPopulation(startingPop);
            btnDefaultStart.Enabled = false; 
        }
    }
}
