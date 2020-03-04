using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    class Simulation
    {
        private Trial trialResult;
        private int trialNumber;

        public int TrialNumber
        {
            get { return trialNumber; }
            set { trialNumber = value; }
        }

        public Trial TrialResult
        {
            get { return trialResult; }
            set { trialResult = value; }
        }


        //constructor
        public Simulation (int n, Trial t)
        {
            trialResult = t;
            trialNumber = n;
        }

        public Simulation() { }

        //methods

        //Get end pop
        private int EndingPop(List<Trial> t)
        {
            int endPop = t[t.Count - 1].MonthResults.PopEnd;
            return endPop;
        }
    }
}
