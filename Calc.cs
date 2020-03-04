using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVAfront
{
    public class Calc //keep any math functions in here 
    {
        Random rnd = new Random(); 

        public bool CoinFlip(double prob) //function for outcome simulation for binary events (AMR/stable, life/death, birth/no birth) 
        {
            Random rnd = new Random();
            if (rnd.NextDouble() < prob) //if random number is less than your probability, event happened 
            {
                return true;
            }
            else return false; //random number > probability, event didnt happen 
        }

        public double SampleBeta(double mean, double sd) //function for sampling from beta distribution 
        {
            //taken from James McCaffrey, based on 1978 paper from RCH Cheng. 
            //https://jamesmccaffrey.wordpress.com/2017/11/01/more-on-sampling-from-the-beta-distribution-using-c/cheng_ba_csharp/
            // Assumption: mean, sd greater-than 0

            double alpha = mean + sd;
            double beta = 0.0;
            double u1, u2, w, v = 0.0;

            if (Math.Min(mean, sd) <= 1.0)
                beta = Math.Max(1 / mean, 1 / sd);
            else
                beta = Math.Sqrt((alpha - 2.0) / (2 * mean * sd - alpha));
            double gamma = mean + 1 / beta;
            while (true)
            { 
                u1 = this.rnd.NextDouble();
                u2 = this.rnd.NextDouble();
                v = beta * Math.Log(u1 / (1 - u1));
                w = mean * Math.Exp(v);
                double tmp = Math.Log(alpha / (sd + w));
                if (alpha * tmp + (gamma * v) - 1.3862944 >= Math.Log(u1 * u1 * u2))
                    break;
            }
            double x = w / (sd + w);
            return x;
        } 




        //need to build a function that counts the number of individuals in the population, maybe per age class. Do that here or in different class? 
        int PopCount(List<simpleInd> popInd)
        {
            int popCount = popInd.Count();
            return popCount;
        }
    } 
}

