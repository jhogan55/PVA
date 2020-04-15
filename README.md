# PVA
Capuchin Population Viability Analysis

Most of the classes from the Visual Studio/C# windows form application designed to simulate capuchin populations through time. 
The goal of this simulation is to determine what effect (if any) male takeovers have on the population long term. The program accepts input for a starting population, then conducts stepwise monthly survival and reproductive checks for each individual in the population. Survival rates are sampled from a beta distribution, with the input being values calculated from real life capuchins (Santa Rosa study groups over the past 35 years). 

The basic stepwise simulation occurs as follows: 
1. Age up all individuals in the population by a month. Test mother-infant dependencies
  - If an infant reaches adolescence (currently 12 months), remove infant dependency blocks from mother, and mother ID from infant (so that they aren't auto-killed if mom dies). 

2. Give birth to any infants that have been gestating for 6 months. Determine sex of infant and, if female, add to population. 

3. Determine if an AMR occurs this month using a weighted coin flip (AMR rate in Santa Rosa being the coin flip)

4. If AMR occurs, "risk period" begins. 

5. If no AMR occurs, but you are still in a risk period, the risk period counter increments by 1. If risk period exceeds the specified length (currently 6 months), risk period is turned off 

6. Determine life and death for each individual in the population for the month 
  - Real-world survival mean and standard deviation for the individual's age class are fed to the beta sampler, which returns a   randomly drawn sample. This is then used for a weighted coin flip. At present, the risk is equal for everyone within an age class (i.e., only 1 sample is drawn/month/age class). This is because the environmental stochasticity we are tying to simulate should roughly affect everyone equally.
  - If individual dies, check if the individual is a mother with a dependent infant. If so, kill the infant too. 
  - If individual is an infant, remove "dependent infant" checks from mother and put her on the shortened IBI track. 

7. Reproductive opportunities. To be eligible you must be an adult without dependent infant. Similar setup to survival: real-world mean reproductive and sd rates are fed to beta sampler, random number is drawn and used for weighted coin flip. 

8. Repeat the process for X number of months over Y trials. At completion, program displays what percentage of trials resulted in the number of populations that grew, remained stable, declined, or went extinct. Stable is defined as +/- 3 individuals from starting population, growth is starting pop + 3, declined is starting pop - 3 but not 0, and extinct is 0. 

OTHER NOTES:
- "Default starting pop" is based on LV group in March, 2020: 8 adult females, 4 juveniles and 2 infant males. 
- There is no age-curve at the moment (eg, older adults are not penalized for survival or reproductive opportunity. 
- There is no density-dependence regulation in play for population. Not sure whether there should be or not. 
- The only variable that currently changes with AMR risk is infant mortality. Lots more babies should die during AMR periods.
- Infant males are tracked solely to determine which IBI track mom is on. After infancy, males are ignored. 
- AMR replacements are independent of each other, so theoretically could occur back to back to back to back to... you get the idea. Not sure at the moment whether this is the best approach. 

FUTURE WORK: 
- The results from each month and trial are currently stored in their respective classes during a simulation, but arent accessible to users. Probably want to create an "export results" button that would print a csv file with each month and trial. Simple 7 columns is what im thinking: Trial/Month/AmrOccur/RiskMonth/Pop/Births/Deaths 
- Add variables to mom: total number of infants
- Add tracking variables: rate of infant mortality in AMRs vs Stable periods etc. 
- Allow user to alter vital rates besides AMR (which is now available on the main form) 
