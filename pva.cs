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
                        newInd.MonthsSinceBirth = infAge; //add 6 months of pregnancy time to age  

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

                else //no dependent infant info added, just add individual to population as is. 
                {
                    newInd.MonthsSinceBirth = 12; //no dependent, so assume it has been at least a year since ind had an infant 
                    startingPop.Add(newInd); 

                }
                RefreshPopulation(startingPop, lstPop, txtStartPop);
                ResetForm();
            }
           
        }

        //Start simulation 
        private void btnRun_Click(object sender, EventArgs e)
        {
            //grab years and trials
            if (Validator.TextEntered(txtYears, "Years") && Validator.TextEntered(txtTrials, "Trials") && Validator.TextEntered(txtAmr, "AMR Rate")
                && Validator.IsInt(txtYears, "Years") && Validator.IsInt(txtTrials, "Trials") && Validator.IsDouble(txtAmr, "AMR Rate") 
                && Validator.WithinRange(txtYears,0, 1000, "Years") && Validator.WithinRange(txtTrials, 0, 100000, "Trials") && Validator.WithinRange(txtAmr, 0, 1, "AMR Rate"))
            {               
                int years = Convert.ToInt32(txtYears.Text);
                int trials = Convert.ToInt32(txtTrials.Text);
                double amrRate = Convert.ToDouble(txtAmr.Text); //rate to use for AMRs
                int months = years * 12;               
                RunSim(startingPop, months, trials, amrRate);
                //countID = 0;
                ResetForm();
            }
           
        }

        //function: run sim
        private void RunSim(List<simpleInd> p, int m, int t, double amr)
        {
            List<Trial> trialList = new List<Trial>();
            
            //counters used at end of sim to display crude results 
            int biggerPops = 0; 
            int extinctPops = 0;
            int smallerPops = 0;
            int neutralPops = 0; 
            
            
            for (int i = 0; i < t; i++) //Trial loop: run for the number of trials 
            {              
                Trial trial = new Trial(); //new trial has started, add monthly results to this 
                List<simpleInd> trialPop = new List<simpleInd>(p); //population to change within trial
                lblRunning.Text = "Conducting trial " + i.ToString() + " of " + t;
                lblRunning.Invalidate();
                lblRunning.Update();
                //Risk related variables
                int riskLength = VitalRates.RISKPERIOD; //counter for AMRs, assumes month 0 of sim was NOT a takeover month so set counter above threshold 
                bool amrPeriod = false; //start of sim, default is no risk 

                
                for (int j = 0; j < m; j++) //Month loop: run until you reach the # months 
                {
                    //Step 1: age everyone up a month and add a month to necessary counters 
                    AgeUp(trialPop); //add a month to everyone's age 
                    trial.MonthOfTrial = j + 1; //this is the month within the trial 

                    //Step 2: Give birth
                    GiveBirth(trialPop); //turn pregnancies that are long enough into babies

                    //Step 3: Decide if takeover occurred and if you're still under risk period 
                    bool takeover = calc.CoinFlip(amr); //weighted coin flip for a new takeover 
                    
                    if (takeover) //takeover has occurred 
                    {
                        riskLength = 0; //reset the risk period length to 0
                        amrPeriod = true; //you are in a risk period
                    }

                    else riskLength++; //no new takeover but youre in a risk period already, so add a month to risk counter
                    if (riskLength >= VitalRates.RISKPERIOD) amrPeriod = false; //if your risk period counter hits the threshold, turn off the risk variable                                           

                    Month newMonth = new Month(trialPop.Count(), amrPeriod, takeover); //create a month to keep track of variables 

                    //Step 4: generate survival rates for each age class based on amrPeriod
                    double adRate = calc.SampleBeta(vr.ReturnSurvMean(2, amrPeriod), vr.ReturnSurvSd(2, amrPeriod));
                    double juvRate = calc.SampleBeta(vr.ReturnSurvMean(1, amrPeriod), vr.ReturnSurvSd(1, amrPeriod));
                    double infRate = calc.SampleBeta(vr.ReturnSurvMean(0, amrPeriod), vr.ReturnSurvSd(0, amrPeriod));
                    double rate;

                    //Step 5: flip the weighted coin of death for each ind in your pop
                    for (int k = trialPop.Count -1 ; k >= 0; k--) //loop through pop, can't use foreach because you subtract inds if they die
                    {
                        if (trialPop[k].AgeClass == 2) { rate = adRate; }
                        else if (trialPop[k].AgeClass == 1) { rate = juvRate; }
                        else rate = infRate;
                        bool surv = calc.CoinFlip(rate);
                        //TEST MESSAGE 
                        //MessageBox.Show("For ind " + trialPop[k].IndID + " age class " + trialPop[k].AgeClass +
                        //                ", survival probability = " + rate + ". Survived? " + surv);
                        int? babyToKill = null;
                        if (surv == false) //individual died
                        {
                            //if ind is adult female with dependent infant remove baby too
                            if (trialPop[k].DepInf & trialPop[k].DepInfFem) 
                            {
                                babyToKill = trialPop[k].DepInfID;
                            } //getting baby ID from a dead mom 

                            //if ind is a dependent infant clear record from mom
                            foreach (simpleInd mom in trialPop)
                            {
                                if (mom.IndID == trialPop[k].MomID)
                                {
                                    mom.DepInf = false; //no longer has dependent infant 
                                    mom.DepInfID = 0; //remove infant ID from mom
                                    mom.DepInfFem = false; //i dont think this matters
                                    mom.LastInfSurv = false; //puts mom on the accelerated IBI path
                                } //mom alterations 

                            } //search population for the mom in question  
                            trialPop.RemoveAt(k); //remove ind from population
                            if (babyToKill.HasValue)
                            {
                                trialPop.RemoveAll(x => x.IndID == babyToKill); //lambda expression "remove all instances in trialPop where the Individ X has ID that the DepInfID"
                                newMonth.Deaths++;
                            }
                           
                            newMonth.Deaths++; 
                        } //death loop 
                    }

                    //Step 6: flip the death coin for infant males (only to release blocks from moms) 
                    foreach (simpleInd maleMoms in trialPop)
                    {
                        if (maleMoms.DepInf & maleMoms.DepInfFem == false)
                        {
                            bool surv = calc.CoinFlip(infRate);
                            if (!surv) //baby died, clear blocks from mom 
                            {
                                maleMoms.DepInf = false;
                                maleMoms.LastInfSurv = false;
                                maleMoms.DepInfID = 0; 
                            }
                        }
                    }

                    //Step 7: surviving females have a chance to conceive. 
                    for (int l = trialPop.Count -1; l >= 0; l--)
                    {
                        if (!trialPop[l].Preg) //monthly conception attempt for individs not pregnant. juve and infant likelihood is 0
                        {
                            trialPop[l].Preg = calc.CoinFlip(vr.ReturnReprodMean(trialPop[l])); //weighted coin flip for baby or no 
                        }
                    }

                    newMonth.PopEnd = trialPop.Count();
                    RefreshPopulation(trialPop, lstCurrentPop, txtCurrentPop);
                    //TEST MESSAGE 
                    //MessageBox.Show("End of month. AMR? " + amrPeriod);
                    trial.MonthResults = newMonth;
                                     
                }//End of single month
                
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

        private void RefreshPopulation(List<simpleInd> p, ListBox l, TextBox t)
        {
            l.Items.Clear();
            foreach (simpleInd i in p)
            {
                l.Items.Add(i);
                t.Text = Convert.ToString(l.Items.Count);
            }
        }

        //Age up each ind after every month, verify age classes, click up the counters 
        private void AgeUp(List<simpleInd> p)
        {
            foreach (simpleInd ind in p) //loop through the population 
            {
                ind.Age++; //Add a month to everyone's age
                ind.AgeClass = ind.AssignAgeClass(ind.Age); //Check if anyone "graduated" up an age class or not
                if (!ind.Preg) { ind.MonthsSinceBirth++; } //Anyone who isn't pregnant increase their "time since last baby" by a month 
                if (ind.Preg) //for pregnant females, add a month to their pregnancy and make sure months since birth remains at 0
                {
                    ind.PregDuration++;
                    ind.MonthsSinceBirth = 0; //make sure "MonthsSinceBirth" remains at 0 for pregnant females 
                } //pregnancy advances a month.              
            }
            DependencyCheck(p); //for any infant that reaches 12 months, remove dependency to mom 
        }

        //pregnancy has reached the gestation length, turn off pregnancy and create a baby 
        private void GiveBirth(List<simpleInd> p)
        {
            List<int> momsWithFems = new List<int>();
            foreach (simpleInd ind in p)
            {
                if (ind.PregDuration >= VitalRates.GESTATION) //If pregDuration = gestation vital rate, create new baby
                {
                    
                    ind.Preg = false;
                    ind.PregDuration = 0; //reset pregnancy counter 
                    ind.DepInf = true; //turn on dep inf 
                    ind.DepInfFem = calc.CoinFlip(VitalRates.SEXRATIO); //determine sex of baby 
                    if (ind.DepInfFem)
                    {
                        momsWithFems.Add(ind.IndID); //keep track of which females need a baby linked to them once you escape the foreach
                    }
                }
            }
            foreach(int momID in momsWithFems) //use the caught momIDs to create new female infants
            {
                AddToPop(p, momID); 
            }
        }

        //add an individual to the population (right now only infant females) 
        private void AddToPop(List<simpleInd> p, int momID)
        {
            simpleInd baby = new simpleInd(countID, 0, 0, false, 0, false, 0, momID); //create new female, link her to mom
            foreach (simpleInd ind in p)
            {
                if (ind.IndID == momID) { ind.DepInfID = baby.IndID; } 
            }
            p.Add(baby); //add baby to pop
            countID++; //increment counter 
        }

        private void DependencyCheck(List<simpleInd> p)
        {
            foreach (simpleInd ind in p)
            {
                if (ind.Age >= 12 & ind.MomID > 0) //if an infant "graduates" to juvenile remove dependency on mom
                {
                    foreach (simpleInd i in p)
                    {
                        if (i.IndID == ind.MomID)
                        {
                            i.DepInfID = 0; //infant no longer dies if mom dies
                            i.DepInf = false; //mom no longer has a dependent infant 
                            i.LastInfSurv = true; //Mom is on the "slow/normal" IBI track 
                        }
                    }
                }

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

            simpleInd sals = new simpleInd(1, 2, 296, true, 0, false, 11, 0); //male dep inf
            simpleInd chut = new simpleInd(2, 2, 253, false, 0, false, 12, 0);
            simpleInd oreg = new simpleInd(3, 2, 179, true, 0, false, 10, 0); // male dep inf 
            simpleInd chch = new simpleInd(4, 2, 190, false, 0, false, 12, 0);
            simpleInd thym = new simpleInd(5, 2, 147, false, 0, false, 12, 0);
            simpleInd vani = new simpleInd(6, 2, 123, false, 0, false, 12, 0);
            simpleInd sage = new simpleInd(7, 2, 121, false, 0, false, 12, 0);
            simpleInd crys = new simpleInd(8, 2, 93, false, 0, false, 12, 0);
            simpleInd roux = new simpleInd(9, 1, 72, false, 0, false, 12, 0);
            simpleInd fres = new simpleInd(10, 1, 72, false, 0, false, 12, 0);
            simpleInd papr = new simpleInd(11, 1, 37, false, 0, false, 12, 0);
            simpleInd hari = new simpleInd(12, 1, 19, false, 0, false, 12, 0);

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
            RefreshPopulation(startingPop, lstPop, txtStartPop);
            btnDefaultStart.Enabled = false; 
        }

        private void btnEditRates_Click(object sender, EventArgs e)
        {
            frmVitalRates vr = new frmVitalRates();
            vr.Show();
        }

        private void btnAmrUpdate_Click(object sender, EventArgs e)
        {
          
        }

        private void btnSampleTest_Click(object sender, EventArgs e)
        {
            SampleTest st = new SampleTest();
            st.SampleDistribution();
        }
    }
}

