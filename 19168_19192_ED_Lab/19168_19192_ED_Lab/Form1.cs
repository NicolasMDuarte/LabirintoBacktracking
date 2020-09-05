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

        private void btnAbrir_Click(object sender, EventArgs e) //Abriu o labirinto
        {
            string arquivo;
            string linha;
            int linhaAtual = -1;
            int cols;
            int lis;
            char[,] matriz;

            if(dlgAbrir.ShowDialog() == DialogResult.OK) //Selecionou um arquivo
            {
                dgvLab.Columns.Clear(); //Limpa colunas
                arquivo = dlgAbrir.FileName;
                StreamReader leitor = new StreamReader(arquivo);
                cols = int.Parse(leitor.ReadLine());
                lis = int.Parse(leitor.ReadLine());
                dgvLab.ColumnCount = cols; //Adiciona as colunas

                matriz = new char[lis, cols]; //Cria uma matriz
                while(!leitor.EndOfStream)
                {
                    linhaAtual++;
                    linha = leitor.ReadLine();

                    dgvLab.Rows.Add(linha); //Adiciona a linha no dgv
                    for(int i = 0; i < cols; i++)
                    {
                        matriz[linhaAtual, i] = linha[i]; //Adiciona a linha na matriz
                        dgvLab[i, linhaAtual].Value = linha[i]; //Adiciona a linha no dgv
                        dgvLab.Columns[i].Width = 27; //Define a largura da coluna como 18
                        dgvLab.Columns[i].DefaultCellStyle.Font = new Font("Arial", 15, FontStyle.Regular);
                    }
                }
                
                labirinto = new Labirinto(matriz, lis, cols); //Cria um novo Labirinto
            }
        }

        private void btnEncontrar_Click(object sender, EventArgs e) //Apertou para encontrar caminho
        {
            Solucionadora soluc = new Solucionadora(labirinto);

            soluc.AcharCaminhos(ref dgvLab); //Acha os caminhos
            soluc.MostrarCaminhos(ref dgvCaminhos); //Exibe os caminhos
        }
    }
}
