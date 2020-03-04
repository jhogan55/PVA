using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    class Trial
    {
        private int monthOfTrial = 1;
        private Month monthResults;
        private int totalBirths;
        private int totalDeaths;

        public int MonthOfTrial
        {
            get { return monthOfTrial; }
            set { monthOfTrial = value; }
        }

        public int TotalBirths
        {
            get { return totalBirths; }
            set { totalBirths = value; }
        }

        public int TotalDeaths
        {
            get { return totalDeaths; }
            set { totalDeaths = value; }
        }

        public Month MonthResults
        {
            get { return monthResults; }
            set { monthResults = value; }
        }

        //Constructors
        public Trial() { }

        public Trial (int trialMonth, Month results)
        {
            monthOfTrial = trialMonth;
            monthResults = results;
        }

    }
    
}
