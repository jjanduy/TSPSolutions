using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSPSolutions.View
{
    public partial class TSPSolutions : Form
    {
        Bitmap img = new Bitmap(500, 400, PixelFormat.Format32bppArgb);
        int[][] adjacencyMatrix;
        Point[] cidades;
        int[] caminho;
        string descricao;

        public TSPSolutions()
        {
            InitializeComponent();

            string[] lista = Directory.GetFiles("./data", "*.tsp");

            for (int i = 0; i < lista.Length; i++)
            {
                comboBox1.Items.Add(Path.GetFileNameWithoutExtension(lista[i]));
            }

            pictureBox1.Image = (Image)img.Clone();
            comboBox3.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dir = string.Format("./data/{0}.tsp", comboBox1.SelectedItem);

            Utils.Utils.getDistances(dir, out adjacencyMatrix, out cidades, out descricao);
            resizePoints();
            textBox1.Text = descricao;

            desenhaCidades();

            
        }

        private void desenhaCidades()
        {
            pictureBox1.Image = (Image)img.Clone();

            using (Graphics gp = Graphics.FromImage(pictureBox1.Image))
            {
                var sBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
                for (int i = 0; i < cidades.Length; i++)
                {
                    gp.FillEllipse(sBrush, cidades[i].X - 1, cidades[i].Y - 1, 3, 3);
                }
            }
        }

        int passoCaminho = 0;

        private void desenhaCaminho()
        {
            using (Graphics gp = Graphics.FromImage(pictureBox1.Image))
            {
                Pen redPen = new Pen(Color.Red, 0.5f);

                gp.DrawLine(redPen, cidades[caminho[passoCaminho]], cidades[caminho[(passoCaminho + 1) % (caminho.Length - 1)]]);

                passoCaminho = (passoCaminho + 1) % (caminho.Length - 1);
            }

            pictureBox1.Refresh();
        }

        private void resizePoints()
        {
            int xMin = int.MaxValue;
            int yMin = int.MaxValue;

            int xMax = int.MinValue;
            int yMax = int.MinValue;

            for (int i = 0; i < cidades.Length; i++)
            {
                if (cidades[i].X < xMin)
                    xMin = cidades[i].X;
                if (cidades[i].X > xMax)
                    xMax = cidades[i].X;

                if (cidades[i].Y < yMin)
                    yMin = cidades[i].Y;
                if (cidades[i].Y > yMax)
                    yMax = cidades[i].Y;
            }

            xMin -= 5;
            yMin -= 5;

            int difX = xMax - xMin;
            int difY = yMax - yMin;

            for (int i = 0; i < cidades.Length; i++)
            {
                cidades[i].X = (int)((cidades[i].X - xMin) * 480.0 / difX);
                cidades[i].Y = (int)((cidades[i].Y - yMin) * 380.0 / difY);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (passoCaminho == 0)
                desenhaCidades();

            desenhaCaminho();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = 450 - comboBox3.SelectedIndex * 200;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime t_inicio = DateTime.Now;
            DateTime t_fim = DateTime.Now;

            if (comboBox2.SelectedIndex == 0)
            {
                t_inicio = DateTime.Now;
                caminho = Algorithms.BuscaProfundidade.tsp(adjacencyMatrix, 0);
                t_fim = DateTime.Now;
                lDist.Text = caminho[caminho.Length - 1] + "";

                timer1.Enabled = true;
                timer1.Start();
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                //caminho = Algorithms.GA.tsp(cidades, adjacencyMatrix, 0);
                t_inicio = DateTime.Now;

                caminho = Algorithms.Guloso.tsp(adjacencyMatrix, 0);
                t_fim = DateTime.Now;

                lDist.Text = caminho[caminho.Length - 1] + "";

                timer1.Enabled = true;
                timer1.Start();
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                t_inicio = DateTime.Now;
                caminho = Algorithms.AStar.tsp(adjacencyMatrix, 0);
                t_fim = DateTime.Now;

                lDist.Text = caminho[caminho.Length - 1] + "";

                timer1.Enabled = true;
                timer1.Start();
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                t_inicio = DateTime.Now;
                caminho = Algorithms.GA.tsp(cidades, adjacencyMatrix, 0);
                t_fim = DateTime.Now;
                lDist.Text = caminho[caminho.Length - 1] + "";
            }

            TimeSpan t_diferenca = t_fim.Subtract(t_inicio);
            label5.Text = t_diferenca.TotalSeconds.ToString("0.000000") + " segundos";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            desenhaCidades();

        }
    }
}
