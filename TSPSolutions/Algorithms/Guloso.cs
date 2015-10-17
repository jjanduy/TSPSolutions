using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms
{
    class Guloso
    {
        public static int[] tsp(int[][] adjacencyMatrix, int inicio)
        {
            int numberOfNodes = adjacencyMatrix[1].Length;
            int[] visited = new int[numberOfNodes];
            int[] caminho = new int[numberOfNodes+1];
            visited[inicio] = 1;
            int aux = 0;
            caminho[aux++] = inicio;
            int stack = inicio;
            int element = 0, dst = 0, i;
            int min = int.MaxValue;
            bool minFlag = false;
            //Console.WriteLine(inicio + "\t");

            int percorrido = 0;

            while (stack >= 0)
            {
                element = stack;
                i = 0;
                min = int.MaxValue;

                stack = -1;

                while (i < numberOfNodes)
                {
                    if (adjacencyMatrix[element][i] > 0 && visited[i] == 0)
                    {
                        if (min > adjacencyMatrix[element][i])
                        {
                            min = adjacencyMatrix[element][i];
                            dst = i;
                            minFlag = true;
                        }
                    }
                    i++;
                }
                if (minFlag)
                {
                    visited[dst] = 1;
                    caminho[aux++] = dst;
                    stack = dst;
                    //Console.Write(dst + "\t");
                    percorrido += min;
                    minFlag = false;
                    continue;
                }
            }

            percorrido += adjacencyMatrix[element][inicio];

            caminho[aux++] = percorrido;

            return caminho;
        }

    }
}
