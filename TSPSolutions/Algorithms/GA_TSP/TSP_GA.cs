using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms.GA_TSP
{
    class TSP_GA
    {
        private static double mutationRate = 0.2;//0.015;
        private static int tournamentSize = 5;
        private static bool elitism = true;

        static Random rdm1 = new Random();


        // Evolves a population over one generation
        public static Population evolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.populationSize(), false);

            // Keep our best individual if elitism is enabled
            int elitismOffset = 0;
            if (elitism)
            {
                newPopulation.saveTour(0, pop.getFittest());
                elitismOffset = 1;
            }

            // Crossover population
            // Loop over the new population's size and create individuals from
            // Current population
            for (int i = elitismOffset; i < newPopulation.populationSize()-1; i++)
            {

                // Select parents
                Tour parent1 = tournamentSelection(pop);
                Tour parent2 = tournamentSelection(pop);
                // Crossover parents
                //if (rdm1.NextDouble() < 0.8)
                //{
                    Tour[] child = crossover(parent1, parent2);
                    // Add child to new population
                    newPopulation.saveTour(i++, child[0]);
                    newPopulation.saveTour(i, child[1]);
                //}
                //else
                    //newPopulation.saveTour(i, parent1);
            }

            Tour parent3 = tournamentSelection(pop);
            Tour parent4 = tournamentSelection(pop);
            // Crossover parents
            //if (rdm1.NextDouble() < 0.8)
            //{
            Tour[] child1 = crossover(parent3, parent4);
            // Add child to new population
            newPopulation.saveTour(newPopulation.populationSize() - 1, child1[0]);

            // Mutate the new population a bit to add some new genetic material
            for (int i = elitismOffset; i < newPopulation.populationSize(); i++)
            {
                if (rdm1.NextDouble() < mutationRate)
                {
                    mutate(newPopulation.getTour(i));
                }
            }

            return newPopulation;
        }

        // Applies crossover to a set of parents and creates offspring
        public static Tour[] crossover(Tour parent1, Tour parent2)
        {
            // Create new child tour
            Tour[] child = new Tour[2];
            child[0] = new Tour();
            child[1] = new Tour();
            
            // Get start and end sub tour positions for parent1's tour
            int startPos = rdm1.Next(parent1.tourSize());
            int endPos = rdm1.Next(startPos, parent1.tourSize());

            //startPos = 0;

            // Loop and add the sub tour from parent1 to our child
            for (int i = 0; i < child[0].tourSize(); i++)
            {
                if ( i > startPos && i < endPos)
                    child[0].setCity(i, parent1.getCity(i));
                else
                    child[1].setCity(i, parent1.getCity(i));
            }

            // Loop through parent2's city tour
            for (int i = 0; i < parent2.tourSize(); i++)
            {
                // If child doesn't have the city add it
                if (!child[0].containsCity(parent2.getCity(i)))
                {
                    // Loop to find a spare position in the child's tour
                    for (int ii = 0; ii < child[0].tourSize(); ii++)
                    {
                        // Spare position found, add city
                        if (child[0].getCity(ii) == null)
                        {
                            child[0].setCity(ii, parent2.getCity(i));
                            break;
                        }
                    }
                }
                else
                {
                    for (int ii = 0; ii < child[1].tourSize(); ii++)
                    {
                        // Spare position found, add city
                        if (child[1].getCity(ii) == null)
                        {
                            child[1].setCity(ii, parent2.getCity(i));
                            break;
                        }
                    }
                }
            }
            return child;
        }

        // Mutate a tour using swap mutation
        private static void mutate(Tour tour)
        {
            int tourPos1 = rdm1.Next(tour.tourSize());
            int tourPos2;
            do{
                tourPos2 = rdm1.Next(tour.tourSize());
            }while(tourPos1==tourPos2);

            City city1 = tour.getCity(tourPos1);
            City city2 = tour.getCity(tourPos2);

            // Swap them around
            tour.setCity(tourPos2, city1);
            tour.setCity(tourPos1, city2);
        }



        // Selects candidate tour for crossover
        private static Tour tournamentSelection(Population pop)
        {

            // Create a tournament population
            Population tournament = new Population(tournamentSize, false);
            // For each place in the tournament get a random candidate tour and
            // add it
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomId = rdm1.Next(pop.populationSize());
                tournament.saveTour(i, pop.getTour(randomId));
                //Console.WriteLine(randomId);
            }
            // Get the fittest tour
            Tour fittest = tournament.getFittest();
            return fittest;
        }
    }
}
