
//http://comopt.ifi.uni-heidelberg.de/


//http://www.theprojectspot.com/tutorial-post/applying-a-genetic-algorithm-to-the-travelling-salesman-problem/5

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TSPSolutions.Utils
{
    class Utils
    {
        public static void getDistances(string fileDir, out int[][] adjacency_matrix, out Point[] cidades, out string descricao)
        {
            string[] lines = File.ReadAllLines(fileDir);

            int type = 0;

            int nCidades = 0;

            descricao = "";
            int contador;
            for (contador = 0; contador < lines.Length; contador++)
            {
                if (lines[contador].Contains("DIMENSION"))
                {
                    string[] aux = lines[contador].Trim().Split(' ');
                    nCidades = int.Parse(aux[aux.Length - 1]);
                }
                else if (lines[contador].Contains("EDGE_WEIGHT_TYPE"))
                {
                    if (lines[contador].Contains("EUC_2D"))
                        type = 1;
                    else if (lines[contador].Contains("EXPLICIT"))
                        type = 0;
                }
                else if (lines[contador].Contains("NODE_COORD_SECTION") || lines[contador].Contains("EDGE_WEIGHT_SECTION"))
                {
                    contador++;
                    break;
                }

                descricao += lines[contador] + Environment.NewLine;
            }

            cidades = new Point[nCidades];
            adjacency_matrix = new int[nCidades][];

            for (int j = 0; j < nCidades; j++)
            {
                adjacency_matrix[j] = new int[nCidades];
            }

            if(type == 0)
            {
                for (int i = 0; i < nCidades - 1; i++)
                {
                    int cont = 0;
                    string[] val = lines[contador++].Split(' ');
                    for (int j = i + 1; j < nCidades; j++)
                    {
                        adjacency_matrix[i][j] = adjacency_matrix[j][i] = int.Parse(val[cont++]);
                    }
                }

                cidades[0].X = 0;
                cidades[0].Y = 0;

                cidades[1].X = adjacency_matrix[0][1];
                cidades[1].Y = 0;
                
                for (int i = 2; i < nCidades; i++)
                {
                    double ab = Math.Pow(adjacency_matrix[0][1] , 2.0);
                    double ac = Math.Pow(adjacency_matrix[0][i] , 2.0);
                    double bc = Math.Pow(adjacency_matrix[1][i] , 2.0);

                    double cx = (( ab + ac - bc) / (2 * adjacency_matrix[0][1]));

                    cidades[i].X = (int)cx;

                    double cy = (ac - (cx * cx) > 0) ? Math.Sqrt(ac - (cx * cx)) : 0;

                    double dist1 = Math.Sqrt(Math.Pow(cidades[i].X - cidades[i - 1].X, 2) + Math.Pow( cy - cidades[i - 1].Y, 2));
                    double dist2 = Math.Sqrt(Math.Pow(cidades[i].X - cidades[i - 1].X, 2) + Math.Pow(-cy - cidades[i - 1].Y, 2));

                    if(Math.Abs(dist1-adjacency_matrix[i][i-1]) > Math.Abs(dist2-adjacency_matrix[i][i-1]))
                    {
                        cy = -cy;
                    }

                    cidades[i].Y = (int)cy;
                }

            }
            else 
            {
                for (int i = 0; i < nCidades - 1; i++)
                {
                    string[] val = lines[contador++].Split(' ');
                    cidades[i].X = int.Parse(val[1]);
                    cidades[i].Y = int.Parse(val[2]);
                }

                for (int i = 0; i < nCidades; i++)
                {
                    for (int j = i + 1; j < nCidades; j++)
                    {
                        int difx = cidades[i].X - cidades[j].X;
                        int dify = cidades[i].Y - cidades[j].Y;


                        adjacency_matrix[i][j] = adjacency_matrix[j][i] = (int)Math.Sqrt(difx * difx + dify * dify);
                    }
                }
            }

        }

    }
}
