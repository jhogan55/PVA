using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    public class Month
    {
        private bool takeover = false;
        private bool amrPeriod;
        private int popStart;
        private int births = 0;
        private int deaths = 0;
        private int popEnd; 


        public bool Takeover
        {
            get
            {
                return takeover;
            }
            set
            {
                takeover = value;
            }
        }

        public bool AmrPeriod
        {
            get { return amrPeriod; }
            set { amrPeriod = value; }
        }

        public int PopStart
        {
            get
            {
                return popStart;
            }
            set
            {
                popStart = value;
            }
        }

        public int Births
        {
            get { return births; }
            set { births = value; }
        }

        public int Deaths
        {
            get { return deaths;  }
            set { deaths = value; }
        }

        public int PopEnd
        {
            get { return popEnd; }
            set { popEnd = value; }
        }

        //constructor
        public Month (int ps, bool a, bool t) 
        {
            popStart = ps;
            amrPeriod = a;
            takeover = t;
            births = 0;
            deaths = 0;
            popEnd = 0;            
        }

        //Methods

        //String override 
        public override string ToString()
        {
            string display;
            if (takeover)  
            {
                display = "Takeover occurred. Starting pop: " + this.PopStart + ", Ending pop: " + this.PopEnd;
            }
            else if (amrPeriod) //risk period 
            {
                display = "No takover, still in risk period. Starting pop: " + this.PopStart + ", Ending pop: " + this.PopEnd;
            }

            else { display = "Stable period. Starting pop: " + this.PopStart + ", Ending pop: " + this.PopEnd; }
            return display;
        }
    }
}
