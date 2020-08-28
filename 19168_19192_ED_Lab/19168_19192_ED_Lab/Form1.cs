using _19168_19192_ED_Lab.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19168_19192_ED_Lab
{
    public partial class Form1 : Form
    {
        private Labirinto labirinto;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            string arquivo;
            string linha;
            int linhaAtual = -1;
            int cols;
            int lis;
            char[,] matriz;

            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                dgvLab.Columns.Clear();
                arquivo = dlgAbrir.FileName;
                StreamReader leitor = new StreamReader(arquivo);
                cols = int.Parse(leitor.ReadLine());
                lis = int.Parse(leitor.ReadLine());
                dgvLab.ColumnCount = cols;

                matriz = new char[lis, cols];
                while(!leitor.EndOfStream)
                {
                    linhaAtual++;
                    linha = leitor.ReadLine();

                    dgvLab.Rows.Add(linha);
                    for(int i = 0; i < cols; i++)
                    {
                        matriz[linhaAtual, i] = linha[i];
                        dgvLab[i, linhaAtual].Value = linha[i];
                        dgvLab.Columns[i].Width = 18;
                    }
                }
                
                labirinto = new Labirinto(matriz, lis, cols);
            }
        }

        private void btnEncontrar_Click(object sender, EventArgs e)
        {
            Solucionadora soluc = new Solucionadora(labirinto, dgvLab, dgvCaminhos);

            soluc.AcharCaminhos();
        }
    }
}
