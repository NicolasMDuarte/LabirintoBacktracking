using _19168_19192_ED_Lab.Classes;
using System;
using System.Collections;
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
        private List<Posicao[]> caminhos;
        // Necessário para o for posterior do dgvCaminhos_CellClick, evitando erros desnecessários.
        private int[] qtdPosicoesEmCadaCaminho;

        public Form1()
        {
            InitializeComponent();
            caminhos = new List<Posicao[]>();
        }

        private void btnAbrir_Click(object sender, EventArgs e) //Abriu o labirinto
        {
            // Resetar o dgvCaminhos para não ocorrer confusões 
            // entre o usuário - Clicando em uma Cell no meio da abertura 
            // do arquivo ou no meio da ação de encontrar os caminhos
            dgvCaminhos.Columns.Clear();
            dgvCaminhos.Rows.Clear();

            string arquivo;
            string linha;
            int linhaAtual = -1;
            int cols;
            int lis;
            char[,] matriz;

            if(dlgAbrir.ShowDialog() == DialogResult.OK) //Selecionou um arquivo
            {
                arquivo = dlgAbrir.FileName;
                StreamReader leitor = new StreamReader(arquivo);
                cols = int.Parse(leitor.ReadLine());
                lis = int.Parse(leitor.ReadLine());

                dgvLab.Columns.Clear(); //Limpa colunas do dgvLab
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
                        dgvLab.Columns[i].Width = 27; //Define a largura da coluna como 27
                        dgvLab.Columns[i].DefaultCellStyle.Font = new Font("Arial", 15, FontStyle.Regular); // Define o estilo de letra das Células do dgvLab
                    }
                }
                // Tira o foco padrão do dgv.
                dgvLab[0, 0].Selected = false;

                labirinto = new Labirinto(matriz, lis, cols); //Cria um novo Labirinto
            }
        }

        private void btnEncontrar_Click(object sender, EventArgs e) //Apertou para encontrar caminho
        {
            btnEncontrar.Enabled = false;
            try
            {
                Solucionadora soluc = new Solucionadora(labirinto);

                soluc.AcharCaminhos(ref dgvLab, ref caminhos, ref qtdPosicoesEmCadaCaminho); //Acha os caminhos
                soluc.MostrarCaminhos(ref dgvCaminhos); //Exibe os caminhos
            }
            catch(Exception)
            {
                MessageBox.Show("Abra um arquivo primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnEncontrar.Enabled = true;
        }

        private void dgvCaminhos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Desabilita o dgvCaminhos para melhorar a interface com o usuário
            dgvCaminhos.Enabled = false;
            Posicao[] caminhoConsultado = caminhos.ElementAt(dgvCaminhos.CurrentRow.Index);

            // Gera um valor aleatório de cor de fundo e de lápis
            Random random = new Random();
            Color corDeFundo = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            Color corDeLapis = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            int tamanhoRealDoVetor = qtdPosicoesEmCadaCaminho[caminhos.IndexOf(caminhoConsultado)];
            // Pintando os passo de cada caminho válido para o labirinto atual..
            for (int i = 0; i < tamanhoRealDoVetor; i++)
            {
                dgvLab[caminhoConsultado[i].Coluna, caminhoConsultado[i].Linha].Style.BackColor = corDeFundo;
                dgvLab[caminhoConsultado[i].Coluna, caminhoConsultado[i].Linha].Style.ForeColor = corDeLapis;
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
            }

            // Abilita o dgvCaminhos de volta
            dgvCaminhos.Enabled = true;
        }
    }
}
