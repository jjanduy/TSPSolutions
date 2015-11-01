using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms.Swarm
{
    class AntColonyOptimization
    {
        public class Ant
        {
            public int[] caminho;
            int atual;
            public int fitness;
            List<int> provaveis;

            public Ant best;

            public Ant()
            {
                caminho = new int[numberOfNodes];
                atual = -1;
                fitness = 0;
                provaveis = new List<int>();

                for (int i = 0; i < numberOfNodes; i++)
                {
                    provaveis.Add(i);
                }
                int pos = rdm.Next(provaveis.Count);
                caminho[++atual] = provaveis[pos];
                provaveis.RemoveAt(pos);
            }

            public void AddCity()
            {
                int pos = rdm.Next(provaveis.Count);
                caminho[++atual] = provaveis[pos];
                provaveis.RemoveAt(pos);
                fitness += adjacencyMatrix[caminho[atual]][caminho[atual - 1]];
            }

            public int cidadeAtual()
            {
                return caminho[atual];
            }

            public void NovoCaminho()
            {
                if (best != null)
                {
                    this.atual = best.atual;
                    this.fitness = best.fitness;
                    this.caminho = (int[])best.caminho.Clone();
                    this.provaveis = best.provaveis.ToList<int>();
                    best = null;
                }
            }
        }

        static Random rdm = new Random();
        static int[][] lista;
        static int numberOfNodes;
        static int[][] adjacencyMatrix;

        public static int[] tsp(int[][] adjacencyMatrix)
        {
            AntColonyOptimization.adjacencyMatrix = adjacencyMatrix;
            numberOfNodes = adjacencyMatrix[1].Length;
            int[] visited = new int[numberOfNodes];
            int[] caminho = new int[numberOfNodes + 1];
            int population = 600;
            int raio = 1000;
            Ant[] colony = new Ant[population];

            //gerarVizinhos(raio);

            for (int i = 0; i < population; i++)
            {
                colony[i] = new Ant();
            }

            for (int i = 0; i < numberOfNodes - 1; i++)
            {
                nextCity(colony);

                segueFeromonio(colony, raio);
            }

            fechaCiclo(colony);

            segueFeromonio(colony, 100000);

            for (int i = 0; i < numberOfNodes; i++)
                caminho[i] = colony[0].caminho[i];

            caminho[numberOfNodes] = colony[0].fitness;

            return caminho;
        }

        private static void fechaCiclo(Ant[] colony)
        {
            foreach (Ant ant in colony)
            {
                ant.fitness += adjacencyMatrix[ant.caminho[0]][ant.caminho[numberOfNodes - 1]];
            }
        }

        private static void segueFeromonio(Ant[] colony, int raio)
        {
            for (int i = 0; i < colony.Length; i++)
            {
                int cont = colony[i].cidadeAtual();
                int fit = colony[i].fitness;
                int aux = -1;

                for (int j = 0; j < colony.Length; j++)
                {
                    int cont2 = colony[j].cidadeAtual();

                    if ((adjacencyMatrix[cont][cont2] < raio) &&
                       (fit > colony[j].fitness)
                       )
                    {
                        aux = j;
                        fit = colony[j].fitness;
                    }
                }

                if (aux != -1)
                    colony[i].best = colony[aux];
            }

            for (int i = 0; i < colony.Length; i++)
            {
                colony[i].NovoCaminho();
            }
        }

        private static void nextCity(Ant[] colony)
        {
            for (int i = 0; i < colony.Length; i++)
            {
                colony[i].AddCity();
            }
        }

        private static void gerarVizinhos(int raio)
        {
            lista = new int[numberOfNodes][];

            for (int i = 0; i < numberOfNodes; i++)
            {
                List<int> aux = new List<int>();
                for (int j = 0; j < numberOfNodes; j++)
                {
                    if (adjacencyMatrix[i][j] < raio && i != j)
                        aux.Add(j);
                }

                lista[i] = aux.ToArray();
            }
        }
    }
}
