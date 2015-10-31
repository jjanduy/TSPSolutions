using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPSolutions.Algorithms.GA_TSP;

namespace TSPSolutions.Algorithms
{
    class GA
    {
        public static int[] tsp(Point[] cidades, int[][] adjacencyMatrix, int inicio)
        {

            TourManager.clear();

            for (int i = 0; i < cidades.Length; i++)
            {
                City city = new City(i, cidades[i].X, cidades[i].Y, adjacencyMatrix[i]);
                TourManager.addCity(city);
            }


            // Initialize population
            Population pop = new Population(3, true);
            Console.WriteLine("Initial distance: " + pop.getFittest().getDistance());

            // Evolve population for 100 generations
            pop = TSP_GA.evolvePopulation(pop);
            for (int i = 0; i < 1000; i++)
            {
                pop = TSP_GA.evolvePopulation(pop);
                Console.WriteLine("Intermediate distance: " + pop.getTour(0).getDistance());
            }

            // Print final results
            Console.WriteLine("Finished");
            Console.WriteLine("Final distance: " + pop.getFittest().getDistance());
            Console.WriteLine("Solution:");
            Console.WriteLine(pop.getFittest().ToString());

            int[] caminho = new int[cidades.Length + 1];

            caminho[caminho.Length - 1] = pop.getFittest().getDistance();

            List<int> per = pop.getFittest().getPercurso();

            for (int i = 0; i < caminho.Length-1; i++)
            {
                caminho[i] = per[i];
            }

            return caminho;
        }
    }
}
