using apBalanceamento;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19168_19192_ED_Lab.Classes
{
    class Solucionadora
    {
        private Labirinto labirinto;
        private DataGridView dgvLab;
        private DataGridView dgvCam;
        private PilhaLista<Posicao> pilha;
        private ArrayList listaCaminhos;
        private char vazio = ' ';
        private char jaPassou = 'j';

        public Solucionadora(Labirinto labirinto, DataGridView dgvLab, DataGridView dgvCam)
        {
            this.labirinto = (Labirinto)labirinto.Clone();
            this.dgvLab = dgvLab;
            this.dgvCam = dgvCam;
            pilha = new PilhaLista<Posicao>();
            listaCaminhos = new ArrayList();
        }

        public void AcharCaminhos()
        {
            Posicao posAtual = new Posicao(1, 1);
            Pintar(1, 1, Color.Blue);
            bool temCaminho;

            while (true)
            {
                temCaminho = TemCaminho(ref posAtual);

                if (temCaminho)
                {
                    dgvLab[posAtual.Coluna, posAtual.Linha].Value = jaPassou;
                    pilha.Empilhar(posAtual);
                    Pintar(posAtual.Linha, posAtual.Coluna, Color.Blue);
                }
                else
                {
                    dgvLab[posAtual.Coluna, posAtual.Linha].Value = vazio;
                    pilha.Desempilhar();
                    Pintar(posAtual.Linha, posAtual.Coluna, Color.White);
                }
            }
        }

        private void Pintar(int lin, int col, Color cor)
        {
            dgvLab[col, lin].Style.BackColor = cor; 
        }

        // Checking the surroundings
        private bool TemCaminho(ref Posicao posAtual)
        {
            // Norte
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == vazio)
            {
                posAtual.Linha -= 1;
            }
            else
            // Nordeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == vazio)
            {
                posAtual.Linha -= 1;
                posAtual.Coluna += 1;
            }
            else
            // Leste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == vazio)
            {
                posAtual.Coluna += 1;
            }
            else
            // Sudeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == vazio)
            {
                posAtual.Linha += 1;
                posAtual.Coluna += 1;
            }
            else
            // Sul
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == vazio)
            {
                posAtual.Linha += 1;
            }
            else
            // Sudoeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == vazio)
            {
                posAtual.Linha += 1;
                posAtual.Coluna -= 1;
            }
            else
            // Oeste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == vazio)
            {
                posAtual.Coluna -= 1;
            }
            else
            // Noroeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == vazio)
            {
                posAtual.Linha -= 1;
                posAtual.Coluna -= 1;
            }
            else
            {
                this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna] = jaPassou;
                return false;
            }

            return true;
        }
    }
}
