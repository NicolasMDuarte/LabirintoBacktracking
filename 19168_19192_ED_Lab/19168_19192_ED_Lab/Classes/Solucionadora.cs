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
        private Labirinto labirinto; //Marcas de passagem aqui

        private PilhaLista<Posicao> pilha;
        private ArrayList listaCaminhos;

        private char vazio = ' ';
        private char jaPassou = 'O';
        private char saida = 'S';
        bool desempilhou = false;
        bool primeiraVez = true;

        public Solucionadora(Labirinto labirinto) //construtor com o labirinto e ambos os dgvs
        {
            this.labirinto = labirinto;
            pilha = new PilhaLista<Posicao>();
            listaCaminhos = new ArrayList();
        }

        public void MostrarCaminhos(ref DataGridView dgvCam) //mostra os caminhos no dgvCam
        {
            foreach (string linha in listaCaminhos)
            {
                string li = linha;
                int tamanho = li.Length;
                string passoDoCaminhoAtual;
                for (int i = 0; i < tamanho; i++)
                {
                    if (li[i] == '|')
                    {
                        passoDoCaminhoAtual = li.Substring(0, i - 1);
                        li = li.Substring(i + 1, tamanho);
                    }
                }
            }
        }

        public void AcharCaminhos(ref DataGridView dgvLab) //encontra os caminhos
        {
            Posicao posAtual = new Posicao(1, 1); //nova posição no início do labirinto
            bool temCaminho;
            bool achouSaida = false;

            while (true) //loop eterno
            {
                try
                {
                    posAtual = (Posicao)pilha.OTopo().Clone();
                }
                catch (Exception)
                {}


                if (!desempilhou)
                    temCaminho = TemCaminho(ref posAtual); //verifica se tem caminho
                else
                    temCaminho = TemCaminhoDesempilhado(ref posAtual);

                if (labirinto.Matriz[posAtual.Linha, posAtual.Coluna] == saida)
                {
                    achouSaida = true;
                    break;
                }


                if (temCaminho) //se tem, avança, empilhando, e pinta
                {
                    dgvLab[posAtual.Coluna, posAtual.Linha].Value = jaPassou;
                    this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna] = jaPassou;

                    pilha.Empilhar(posAtual);
                    desempilhou = false;

                    Pintar(ref dgvLab, posAtual.Linha, posAtual.Coluna);
                }
                else //se não tem, garante o vazio na posição atual e volta
                {
                    dgvLab[posAtual.Coluna, posAtual.Linha].Value = vazio;
                    this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna] = vazio;

                    try
                    {
                        pilha.Desempilhar();
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    desempilhou = true;

                    try
                    {
                        Pintar(ref dgvLab, pilha.OTopo().Linha, pilha.OTopo().Coluna);
                    }
                    catch (Exception)
                    {}
                }

                //CODIGO QUE PARE O LOOP QUANDO TODOS OS CAMINHOS FOREM ENCONTRADOS
                /*
                 * O CODIGO FUNCIONARÁ TENDO COMO BASE QUE, APÓS O PRIMEIRO CAMINHO TER SIDO ENCONTRADO, 
                 * VOLTAREMOS, A PARTIR DO FIM, POSIÇÕES, ENCONTRANDO OS CAMINHOS DE TRÁS PARA FRENTE, ATÉ RETORNARMOS 
                 * AO INÍCIO SEM MAIS NENHUM CAMINHO PARA SER SEGUIDO A PARTIR DELE
                 */
            }

            if (achouSaida)
            {
                /*string ret = "";
                int linhaAtual, colunaAtual;
                while (!pilha.EstaVazia)
                {
                    linhaAtual = pilha.OTopo().Linha;
                    colunaAtual = pilha.OTopo().Coluna;
                    ret += $"Linha:{linhaAtual}, Coluna: {colunaAtual}|";
                }
                ret = ret.Substring(0, ret.Length - 1);
                listaCaminhos.Add(ret);*/
            }
            else
            {
                listaCaminhos.Add("Não há caminhos");
            }
        }

        private void Pintar(ref DataGridView dgvLab, int lin, int col) //NÃO FUNCIONA
        {
            dgvLab[col, lin].Selected = true;
            System.Threading.Thread.Sleep(200); //espera 1 segundo
            Application.DoEvents();
            dgvLab[col, lin].Selected = false;
        }

        private bool TemCaminho(ref Posicao posAtual)
        {
            //CODIGO PARA RETORNAR ALGO QUE PARE O LOOP QUANDO TODOS OS CAMINHOS FOREM ENCONTRADOS


            // Norte
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == vazio || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == saida)
            {
                posAtual.OndeParou = 0;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Linha -= 1;
            }
            else
            // Nordeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == vazio || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == saida)
            {
                posAtual.OndeParou = 1;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Linha -= 1;
                posAtual.Coluna += 1;
            }
            else
            // Leste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == vazio || this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == saida)
            {
                posAtual.OndeParou = 2;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Coluna += 1;
            }
            else
            // Sudeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == vazio || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == saida)
            {
                posAtual.OndeParou = 3;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Linha += 1;
                posAtual.Coluna += 1;
            }
            else
            // Sul
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == vazio || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == saida)
            {
                posAtual.OndeParou = 4;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Linha += 1;
            }
            else
            // Sudoeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == vazio || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == saida)
            {
                posAtual.OndeParou = 5;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Linha += 1;
                posAtual.Coluna -= 1;
            }
            else
            // Oeste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == vazio || this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == saida)
            {
                posAtual.OndeParou = 6;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Coluna -= 1;
            }
            else
            // Noroeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == vazio || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == saida)
            {
                posAtual.OndeParou = 7;
                if (primeiraVez)
                {
                    this.pilha.Empilhar((Posicao) posAtual.Clone());
                    primeiraVez = false;
                }
                posAtual.Linha -= 1;
                posAtual.Coluna -= 1;
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool TemCaminhoDesempilhado(ref Posicao posAtual)
        {
            //CODIGO PARA RETORNAR ALGO QUE PARE O LOOP QUANDO TODOS OS CAMINHOS FOREM ENCONTRADOS


            // Norte
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == vazio || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == saida)
            {
                if (posAtual.OndeParou < 0)
                {
                    posAtual.OndeParou = 0;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Linha -= 1;
                    return true;
                }
            }
            // Nordeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == vazio || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == saida)
            {
                if (posAtual.OndeParou < 1)
                {
                    posAtual.OndeParou = 1;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Linha -= 1;
                    posAtual.Coluna += 1;
                    return true;
                }
            }
            // Leste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == vazio || this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == saida)
            {
                if (posAtual.OndeParou < 2)
                {
                    posAtual.OndeParou = 2;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Coluna += 1;
                    return true;
                }
            }
            // Sudeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == vazio || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == saida)
            {
                if (posAtual.OndeParou < 3)
                {
                    posAtual.OndeParou = 3;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Linha += 1;
                    posAtual.Coluna += 1;
                    return true;
                }
            }
            // Sul
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == vazio || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == saida)
            {
                if (posAtual.OndeParou < 4)
                {
                    posAtual.OndeParou = 4;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Linha += 1;
                    return true;
                }
            }
            // Sudoeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == vazio || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == saida)
            {
                if (posAtual.OndeParou < 5)
                {
                    posAtual.OndeParou = 5;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Linha += 1;
                    posAtual.Coluna -= 1;
                    return true;
                }
            }
            // Oeste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == vazio || this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == saida)
            {
                if (posAtual.OndeParou < 6)
                {
                    posAtual.OndeParou = 6;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Coluna -= 1;
                    return true;
                }
            }
            // Noroeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == vazio || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == saida)
            {
                if (posAtual.OndeParou < 7)
                {
                    posAtual.OndeParou = 7;
                    if (primeiraVez)
                    {
                        this.pilha.Empilhar((Posicao) posAtual.Clone());
                        primeiraVez = false;
                    }
                    posAtual.Linha -= 1;
                    posAtual.Coluna -= 1;
                    return true;
                }
            }
            return false;
        }
    }
}