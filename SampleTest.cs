using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PVAfront
{
    public class SampleTest
    {

        public Calc calcTest = new Calc();
        public string results;
        public void SampleDistribution()
        {
            int[] counts = new int[100];
            for (int i = 0; i < 10000; ++i)
            {
                double p = calcTest.SampleBeta(0.807388, 0.338586);

                if (p > 0.99) ++counts[99];
                else if (p > 0.98) ++counts[98];
                else if (p > 0.97) ++counts[97];
                else if (p > 0.96) ++counts[96];
                else if (p > 0.95) ++counts[95];
                else if (p > 0.94) ++counts[94];
                else if (p > 0.93) ++counts[93];
                else if (p > 0.92) ++counts[92];
                else if (p > 0.91) ++counts[91];
                else if (p > 0.90) ++counts[90];

                else if (p > 0.89) ++counts[89];
                else if (p > 0.88) ++counts[88];
                else if (p > 0.87) ++counts[87];
                else if (p > 0.86) ++counts[86];
                else if (p > 0.85) ++counts[85];
                else if (p > 0.84) ++counts[84];
                else if (p > 0.83) ++counts[83];
                else if (p > 0.82) ++counts[82];
                else if (p > 0.81) ++counts[81];
                else if (p > 0.80) ++counts[80];

                else if (p > 0.79) ++counts[79];
                else if (p > 0.78) ++counts[78];
                else if (p > 0.77) ++counts[77];
                else if (p > 0.76) ++counts[76];
                else if (p > 0.75) ++counts[75];
                else if (p > 0.74) ++counts[74];
                else if (p > 0.73) ++counts[73];
                else if (p > 0.72) ++counts[72];
                else if (p > 0.71) ++counts[71];
                else if (p > 0.70) ++counts[70];

                else if (p > 0.69) ++counts[69];
                else if (p > 0.68) ++counts[68];
                else if (p > 0.67) ++counts[67];
                else if (p > 0.66) ++counts[66];
                else if (p > 0.65) ++counts[65];
                else if (p > 0.64) ++counts[64];
                else if (p > 0.63) ++counts[63];
                else if (p > 0.62) ++counts[62];
                else if (p > 0.61) ++counts[61];
                else if (p > 0.60) ++counts[60];

                else if (p > 0.59) ++counts[59];
                else if (p > 0.58) ++counts[58];
                else if (p > 0.57) ++counts[57];
                else if (p > 0.56) ++counts[56];
                else if (p > 0.55) ++counts[55];
                else if (p > 0.54) ++counts[54];
                else if (p > 0.53) ++counts[53];
                else if (p > 0.52) ++counts[52];
                else if (p > 0.51) ++counts[51];
                else if (p > 0.50) ++counts[50];

                else if (p > 0.49) ++counts[49];
                else if (p > 0.48) ++counts[48];
                else if (p > 0.47) ++counts[47];
                else if (p > 0.46) ++counts[46];
                else if (p > 0.45) ++counts[45];
                else if (p > 0.44) ++counts[44];
                else if (p > 0.43) ++counts[43];
                else if (p > 0.42) ++counts[42];
                else if (p > 0.41) ++counts[41];
                else if (p > 0.40) ++counts[40];

                else if (p > 0.39) ++counts[39];
                else if (p > 0.38) ++counts[38];
                else if (p > 0.37) ++counts[37];
                else if (p > 0.36) ++counts[36];
                else if (p > 0.35) ++counts[35];
                else if (p > 0.34) ++counts[34];
                else if (p > 0.33) ++counts[33];
                else if (p > 0.32) ++counts[32];
                else if (p > 0.31) ++counts[31];
                else if (p > 0.30) ++counts[30];

                else if (p > 0.29) ++counts[29];
                else if (p > 0.28) ++counts[28];
                else if (p > 0.27) ++counts[27];
                else if (p > 0.26) ++counts[26];
                else if (p > 0.25) ++counts[25];
                else if (p > 0.24) ++counts[24];
                else if (p > 0.23) ++counts[23];
                else if (p > 0.22) ++counts[22];
                else if (p > 0.21) ++counts[21];
                else if (p > 0.20) ++counts[20];

                else if (p > 0.19) ++counts[19];
                else if (p > 0.18) ++counts[18];
                else if (p > 0.17) ++counts[17];
                else if (p > 0.16) ++counts[16];
                else if (p > 0.15) ++counts[15];
                else if (p > 0.14) ++counts[14];
                else if (p > 0.13) ++counts[13];
                else if (p > 0.12) ++counts[12];
                else if (p > 0.11) ++counts[11];
                else if (p > 0.10) ++counts[10];

                else if (p > 0.09) ++counts[9];
                else if (p > 0.08) ++counts[8];
                else if (p > 0.07) ++counts[7];
                else if (p > 0.06) ++counts[6];
                else if (p > 0.05) ++counts[5];
                else if (p > 0.04) ++counts[4];
                else if (p > 0.03) ++counts[3];
                else if (p > 0.02) ++counts[2];
                else if (p > 0.01) ++counts[1];
                else ++counts[0];
            }
            for (int i = 0; i < counts.Length; i++)
            {
                results += (counts[i].ToString() + " ");
            }

            MessageBox.Show(results);

        }

    }
}
