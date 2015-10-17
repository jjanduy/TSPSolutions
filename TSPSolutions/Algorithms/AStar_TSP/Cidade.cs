using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms.AStar_TSP
{
    class Cidade
    {
        int[] visitada;
        public Cidade pai;

        List<Cidade> filhas;
        public int id;
        public int custo;

        public Cidade(int[] visitada, int id, Cidade pai)
        {
            this.visitada = (int[]) visitada.Clone();
            this.visitada[id] = 1;
            this.pai = pai;
            this.id = id;
            if (pai != null)
                custo = pai.custo + TSPInfo.adjacencyMatrix[id][pai.id];
            else
                custo = 0;
        }

        public List<Cidade> avancaCaminho()
        {
            filhas = new List<Cidade>();

            for (int i = 0; i < visitada.Length; i++)
            {
                if(visitada[i] == 0)
                {
                    filhas.Add(new Cidade(visitada, i, this));
                }
            }

            return filhas;
        }
    }
}
