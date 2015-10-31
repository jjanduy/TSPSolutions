using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPSolutions.Algorithms.AStar_TSP;

namespace TSPSolutions.Algorithms
{
    class AStar
    {

        static List<Cidade> cidades;
        public static int[] tsp(int[][] adjacencyMatrix, int inicio) {
            TSPInfo.adjacencyMatrix = adjacencyMatrix;
            cidades = new List<Cidade>();
            int numberOfNodes = adjacencyMatrix[1].Length;
            int[] visited = new int[numberOfNodes];
            int[] caminho = new int[numberOfNodes + 1];
            Cidade ini = new Cidade(visited, inicio, null);
            cidades.AddRange(ini.avancaCaminho());
            bool complete = false;
            int value = 0;

            do {
                int min = int.MaxValue;
                for (int i = 0; i < cidades.Count; i++) {
                    int custo = cidades[i].custo + adjacencyMatrix[inicio][cidades[i].id];
                    if (min > custo) {
                        value = i;
                        min = custo;
                    }
                }

                List<Cidade> filhas = cidades[value].avancaCaminho();

                if (filhas.Count == 0) {
                    complete = true;
                } else {
                    cidades.RemoveAt(value);
                    cidades.AddRange(filhas);
                }

            } while (!complete);


            Cidade cid = cidades[value];

            caminho[numberOfNodes] = cid.custo + adjacencyMatrix[inicio][cid.id];
            caminho[0] = inicio;

            for (int i = numberOfNodes - 1; i > 0; i--)
            {
                caminho[i] = cid.id;
                cid = cid.pai;
            }

            return caminho;
        }


    }
}
