using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    public class simpleInd
    {
        //private variables
        //variables related to individual
        private int ageClass; //0 is inf, 1 is juve, 2 is adult
        private int age; //age in months
        private int indID; //individual ID, just to keep track of things
        private bool preg = false; //is female pregnant or not 
        private int pregDuration = 0;//how long female has been pregnant for 

        //variables related to mother/offspring dependency
        private bool depInf; //whether ind has an infant or not (used to restrict pregnancy) 
        private bool depInfFem; //keep track of sex of dependent infants. If its female its added to population, if its male its only tracked via mom
        private bool lastInfSurv; //used to assign reproductive potential for AFs: IBI curve is shorter after infant lost       
        private int monthsSinceBirth; //keep track of how long dependent infant has been around to determine reproductive potential
        private int momID; //keep track of who mom is (used if mom dies, kill dependent infant too)
        private int depInfID; //keep track of any dependents (release mom from pregnancy blocks if infant dies) 

        

        //public properties
        public int AgeClass
        {
            get
            {
                return ageClass;
            }
            set
            {
                if (value >= 0 & value <= 2)
                {
                    ageClass = value;
                }
            }
        }

        public int Age 
        { 
            get
            {
                return age;
            } 
            set
            {
                if (value >= 0 )
                {
                    age = value;
                }
            }
        }


        public bool Preg
        {
            get
            {
                return preg;
            }
            set
            {
                preg = value;
            }
        }

        public int PregDuration
        {
            get
            {
                return pregDuration;
            }
            set
            {
                pregDuration = value;
            }
        }

        public bool DepInf
        {
            get
            {
                return depInf;
            }
            set
            {
                depInf = value;
            }
        }

        public int MonthsSinceBirth
        {
            get
            {
                return monthsSinceBirth;
            }
            set
            {
                monthsSinceBirth = value;
            }
        }

        public int IndID
        {
            get
            {
                return indID;
            }
            set
            {
                if (value >= 0)
                {
                    indID = value;
                }
            }
        }


        public int DepInfID
        {
            get
            {
                return depInfID;
            }
            set
            {
                depInfID = value;
            }
        }

        public bool LastInfSurv
        {
            get
            {
                return lastInfSurv;
            }
            set
            {
                lastInfSurv = value;
            }
        }

        public int MomID
        {
            get
            {
                return momID;
            }
            set
            {
                momID = value;
            }
        }

        public bool DepInfFem
        {
            get
            {
                return depInfFem;
            }
            set
            {
                depInfFem = value;
            }
        }

        //constructors

        //default constructor just to access methods 
        public simpleInd() { }

        //New individual added constructor 
        public simpleInd(int id, int ageClass, int age, bool depInf, int depInfID, bool depInfFem, int monthsSinceBirth, int momID)
        {
            IndID = id;
            AgeClass = ageClass;
            Age = age;
            DepInf = depInf;
            DepInfFem = depInfFem;
            MonthsSinceBirth = monthsSinceBirth;
            DepInfID = depInfID;
            MomID = momID; 
        }

        //methods

        //Check age class and adjust when individual levels up 
        public int AssignAgeClass(int age)
        {
            if (age < VitalRates.JUVEAGE) { ageClass = 0; } //individual is infant
            else if (age >= VitalRates.JUVEAGE & age < VitalRates.ADULTAGE) { ageClass = 1; } //ind is a juve
            else ageClass = 2; //adult 
            return ageClass;
        }

        //string override for list box display 
        public override string ToString()
        {
            string display;
            if (ageClass == 0) //infant 
            {
                display = this.indID + ": infant, " + this.age + " months. Mother ID# " + this.MomID;
            }
            else if (ageClass == 1) //juvenile
            {
                display = this.indID + ": juvenile, " + this.age + " months";
            }

            else //this is an adult 
            {
                if (depInf & preg)
                {
                    display = this.IndID + ": adult, " + this.age + "months. " + this.pregDuration + " months preg AND has dep inf";
                }

                else if (depInf & !preg) //adult has an infant
                {
                    if (depInfFem) //infant is female 
                    {
                        display = this.indID + ": adult, " + this.age + " months. Dependent female infant, ID# " + this.depInfID;
                    }

                    else //infant is male
                    {
                        display = this.indID + ": adult, " + this.age + " months. Dependent male infant";
                    }
                }

                else if (preg & !depInf) //female is pregnant 
                {
                    display = this.IndID + ": adult, " + this.age + "months. " + this.pregDuration + " months preg.";
                }

                else display = this.indID + ": adult, " + this.age + " months. No dependent infant, not pregnant. " + this.monthsSinceBirth + " months since last preg.";
            }
            return display; 
        }

    }
}
