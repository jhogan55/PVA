using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    public class simpleInd
    {
        //simple individual: age, age class, sex, dependent infant 

        //private variables
        //variables related to individual
        private int ageClass; //0 is inf, 1 is juve, 2 is adult
        private int age; //age in months
        private int indID; //individual ID, just to keep track of things

        //variables related to mother/offspring dependency
        private bool depInf; //whether ind has an infant or not (used to restrict pregnancy) 
        private bool depInfFem; //keep track of sex of dependent infants. If its female its added to population, if its male its only tracked via mom
        private int depInfDuration; //keep track of how long reproduction is restricted (18 months total, 6 months preg + 12 months weaning)
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

        public int DepInfDuration
        {
            get
            {
                return depInfDuration;
            }
            set
            {
                depInfDuration = value;
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
        public simpleInd(int id, int ageClass, int age, bool depInf, int depInfID, bool depInfFem, int depInfDuration, int momID)
        {
            IndID = id;
            AgeClass = ageClass;
            Age = age;
            DepInf = depInf;
            DepInfFem = depInfFem;
            DepInfDuration = depInfDuration;
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
        //TODO: need to  improve and expand this to include age class and dependent infants 
        public override string ToString()
        {
            string display;
            if (ageClass == 0) //infant 
            {
                display = this.indID + ": infant, " + this.age + " months";
            }
            else if (ageClass == 1) //juvenile
            {
                display = this.indID + ": juvenile, " + this.age + " months";
            }

            else if (depInf)
            {
                if (depInfFem)
                {
                    display = this.indID + ": adult, " + this.age + " months. Dependent female infant, ID# " + this.depInfID;
                }

                else
                {
                    display = this.indID + ": adult, " + this.age + " months. Dependent male infant";
                }

            }

            else display = this.indID + ": adult, " + this.age + " months. No dependent infant";

            return display; 
        }

    }
}
