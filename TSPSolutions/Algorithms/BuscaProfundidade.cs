using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms
{
    class BuscaProfundidade
    {
        static int[] visited;
        static int raiz;
        static int custo;
        static int numberOfNodes;
        static int[][] adjacencyMatrix;
        static int[] caminho;
        static int[] melhorCaminho;

        static string passeio;

        static int min;

        public static int[] tsp(int[][] adjacencyMatrix, int inicio)
        {
            BuscaProfundidade.adjacencyMatrix = adjacencyMatrix;
            numberOfNodes = adjacencyMatrix[1].Length;
            visited = new int[numberOfNodes];
            caminho = new int[numberOfNodes + 1];
            //visited[inicio] = 1;
            custo = 0;
            cont = 0;
            //passeio = "";

            min = int.MaxValue;

            
                raiz = inicio;
                visited[inicio] = 1;
                caminho[cont++] = inicio;
                buscaProfundidade(inicio);
                //Console.Write("\n{0} -> ", i);
            

            //Console.WriteLine("Custo({0}) = {1}", cont, min);

            melhorCaminho[numberOfNodes] = min;

            return melhorCaminho;
        }

        private static void buscaProfundidade(int pai)
        {
            bool calculate = true;

            for (int i = 0; i < numberOfNodes; i++)
            {
                if (visited[i] == 0)
                {
                    //raiz = i;
                    visited[i] = 1;
                    custo += adjacencyMatrix[pai][i];
                    caminho[cont++] = i;
                    //Console.Write("{0} -> ", i);
                    if(custo < min)
                        buscaProfundidade(i);

                    caminho[--cont] = 0;
                    calculate = false;
                    custo -= adjacencyMatrix[pai][i];
                    visited[i] = 0;
                }
            }

            if (calculate)
            {
                int aux = custo + adjacencyMatrix[pai][raiz];

                if (aux < min)
                {
                    melhorCaminho = (int[])caminho.Clone();
                    min = custo + adjacencyMatrix[pai][raiz];
                }

                //cont++;
            }

            //Console.WriteLine("Custo({0}) = {1}", cont++, min);
        }

        static int cont = 0;
    }
}
