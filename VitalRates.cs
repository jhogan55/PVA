using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    class VitalRates
    {
        //Starting vital rates for Santa Rosa Capuchins, as of Feb 2020. Mar 2020 these are calculated from monthly census records, 1986 to end of 2019
        //Only groups BC, CP, CPAD, CPRM, EX, GN, LV and SE are included due to small samples/temporal gaps from other groups
        public const double AMRRATE = 0.03; //Calculated monthly now 
        public const int RISKPERIOD = 6; //length a risk period lasts for after a takeover (6 months for now)
        public const double SEXRATIO = 0.5; //ratio of males to females at birth. CAN THIS BE REFINED? 

        //NOTE: current juve and adult age "graduations" are set to include a 6 month pregnancy period as "infancy". Will likely want to change this in future iterations
        //The problem with this current format is that an infant in-utero has the same mortality risk as a live one, which is probably not true 
        public const int JUVEAGE = 18; //age at which infant becomes a juvenile (1 year currently) 
        public const int ADULTAGE = 78; //age at which a juvenile becomes an adult (6 years currently) 
        public const int NEUTRALGROWTH = 3; //number of individuals a group can grow or shrink by to be considered "neutral" 
        public const int DEPENDENCY = 18; //length of dependent infants tie to mom: 6 month pregnancy + 12 nursing 
        //public const int PREG = 6; //length of pregnancy. Not in use but will if infant/fetus are split (as they should be) 

        //Adult female survival and fecundity, mean and sd. 
        double afSurvMean = 0.992665;
        double afSurvSd = 0.04144;
        double afReprodMean = 0.042; //how do we decide this? Is it static or increases w/distance from last infant? 
        double afReprodSd = 0.005;

        //Juve female survival, mean and sd
        double juvSurvMean = 0.996619;
        double juvSurvSd = 0.026448;
        
        //Infant female survival, mean and sd, during stable and AMR periods 
        double infSurvStbMean = 0.969926;
        double infSurvStbSd = 0.136259;
        double infSurvAmrMean = 0.807388;
        double infSurvAmrSd = 0.338586;

        //method to assign the correct mean survival based on 1) age class and 2) whether its AMR period 
        public double ReturnSurvMean(int i, bool a) 
        {
            double mean;
            if (i == 0) //infant
            {
                if (a) //true = AMR event
                {
                    mean = infSurvAmrMean;
                }
                else
                {
                    mean = infSurvStbMean;
                }
            }
            else if (i == 1) //juv 
            {
                mean = juvSurvMean;
            }
            else mean = afSurvMean;
            return mean;
        }

        //method to assign the correct sd survival based on 1) age class and 2) whether its AMR period 
        public double ReturnSurvSd(int i, bool a)
        {
            double sd;
            if (i == 0) //infant
            {
                if (a) //true = AMR event
                {
                    sd = infSurvAmrSd;
                }
                else
                {
                    sd = infSurvStbSd;
                }
            }
            else if (i == 1) //juv 
            {
                sd = juvSurvSd;
            }
            else sd = afSurvSd; //this is an adult 
            return sd;
        }

        public double ReturnReprodMean(simpleInd i)
        {
            double repMean;
            if (i.AgeClass < 2) { repMean = 0; }
            else
            {
                repMean = afReprodMean;
            }
            return repMean;
        }

        public double ReturnReprodSd(simpleInd i)
        {
            double repSd;
            if (i.AgeClass < 2) { repSd = 0; }
            else
            {
                repSd = afReprodSd;
            }
            return repSd;
        }

    }
}
