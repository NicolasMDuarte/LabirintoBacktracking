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

        private PilhaLista<Posicao> pilha;
        private ArrayList listaCaminhos;

        private const char VAZIO = ' ';
        private const char JA_PASSOU = 'O';
        private const char SAIDA = 'S';

        public Solucionadora(Labirinto labirinto) //construtor com o labirinto e ambos os dgvs
        {
            this.labirinto = labirinto;
            pilha = new PilhaLista<Posicao>();
            listaCaminhos = new ArrayList();
        }

        // Organiza o dgvCam para mostrar cada passo que o algoritmo deu em forma de String, dividindo 
        // cada uma das "linhas" de passos que vieram do método AcharCaminhos pela ArrayList "listaCaminhos".
        public void MostrarCaminhos(ref DataGridView dgvCam) //mostra os caminhos no dgvCam
        {
            dgvCam.Enabled = true;
            int qtdLinhas = 0;
            dgvCam.Columns.Clear();
            foreach (string linha in listaCaminhos)
            {
                if(linha == "Não há caminhos")
                {
                    dgvCam.ColumnCount = 1;
                    dgvCam.Rows.Add("Não há caminhos");
                    dgvCam.Enabled = false;
                    break;
                }
                string li = linha;
                string passoDoCaminhoAtual;
                int qtdPassos = 0;

                dgvCam.RowCount += 1;
                for (int i = 0; i < li.Length; i++)
                {
                    if (li[i] == '|')
                    {
                        passoDoCaminhoAtual = li.Substring(0, i);
                        
                        li = li.Substring(i+1);

                        if(dgvCam.ColumnCount <= qtdPassos)
                            dgvCam.ColumnCount += 1;
                        dgvCam[qtdPassos, qtdLinhas].Value = passoDoCaminhoAtual;
                        qtdPassos++;
                        i = 0;
                    }
                }
                qtdLinhas++;
            }
        }

        // Acha todos os caminhos usando o algoritmo de backtracking de cada labirinto especificado...
        public void AcharCaminhos(ref DataGridView dgvLab, ref List<Posicao[]> caminhos, ref int[] qtdPosicoesEmCadaCaminho) //encontra os caminhos
        {
            Posicao posAtual = new Posicao(1, 1); //nova posição no início do labirinto
            Posicao[] vetorPos = new Posicao[labirinto.Matriz.Length];
            qtdPosicoesEmCadaCaminho = new int[labirinto.Matriz.Length];
            caminhos = new List<Posicao[]>();
            Posicao proxPosicao =  null;

            bool temCaminho;
            bool achouSaida = false;
            int qtdPosicoes = 0, qtdCaminhos = 0;

            Pintar(ref dgvLab, posAtual.Linha, posAtual.Coluna);

            while (true)
            {
                while (true) //loop eterno
                {
                    proxPosicao = (Posicao)posAtual.Clone();


                    temCaminho = TemCaminho(ref posAtual, ref proxPosicao); //verifica se tem caminho

                    // Sinaliza no labirinto da classe Labirinto e no labirinto visual (dgvLab) para qual lugar prosseguiu
                    // o cursor depois de usar a Rosa dos Ventos (método TemCaminho()).
                    if (temCaminho)
                    {
                        proxPosicao.OndeParou = -1;
                        pilha.Empilhar(posAtual);
                        posAtual = (Posicao)proxPosicao.Clone();

                        // Verifica-se se o lugar para onde o cursor foi é a saída ou não.
                        if ((char)dgvLab[posAtual.Coluna, posAtual.Linha].Value != SAIDA)
                        {
                            dgvLab[posAtual.Coluna, posAtual.Linha].Value = JA_PASSOU;
                            this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna] = JA_PASSOU;
                            Pintar(ref dgvLab, posAtual.Linha, posAtual.Coluna);
                        }
                        else
                        {
                            Pintar(ref dgvLab, posAtual.Linha, posAtual.Coluna);
                            if (listaCaminhos.Count == 0)
                                System.Threading.Thread.Sleep(500);

                            achouSaida = true;
                            break;
                        }
                    }
                    // Volta a posição anterior - pois não há mais lugares para ir da posição atual, a não ser voltar!
                    else
                    {
                        if (pilha.EstaVazia)
                        {
                            Pintar(ref dgvLab, 1, 1);
                            break; //Não há caminhos
                        }

                        proxPosicao = (Posicao)pilha.OTopo().Clone();

                        dgvLab[posAtual.Coluna, posAtual.Linha].Value = VAZIO;
                        this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna] = VAZIO;
                        Pintar(ref dgvLab, proxPosicao.Linha, proxPosicao.Coluna);

                        posAtual = (Posicao)pilha.Desempilhar().Clone();
                    }
                }

                // Quando a saída, no labirinto: "S", é achada pelo algoritmo e o caminho feito para chegar até a saída 
                // é adicionado a ArrayList de strings "listaCaminhos", que será usada posteriormente no método "MostrarCaminhos".
                if (achouSaida)
                {
                    string ret = "";
                    int linhaAtual, colunaAtual;
                    PilhaLista<Posicao> pilhaClonada = pilha.Copia();
                    while (!pilhaClonada.EstaVazia)
                    {
                        linhaAtual = pilhaClonada.OTopo().Linha;
                        colunaAtual = pilhaClonada.OTopo().Coluna;

                        Posicao posicaoAtual = new Posicao(linhaAtual, colunaAtual);
                        vetorPos[qtdPosicoes] = posicaoAtual;

                        ret += $"Linha: {linhaAtual}, Coluna: {colunaAtual}|";
                        pilhaClonada.Desempilhar();
                        qtdPosicoes++;
                    }
                    qtdPosicoesEmCadaCaminho[qtdCaminhos] = qtdPosicoes;
                    caminhos.Add(vetorPos);

                    listaCaminhos.Add(ret);

                    // Reseta-se todas as variáveis usadas para encontrarmos um caminho diferente do já encontrado!
                    qtdPosicoes = 0;
                    vetorPos = new Posicao[labirinto.Matriz.Length];
                    achouSaida = false;
                }
                else
                {
                    if (listaCaminhos.Count > 0)
                    {
                        break; //Encontrou todos os caminhos
                    }
                    else
                        listaCaminhos.Add("Não há caminhos");
                }

                if (pilha.EstaVazia)
                    break;

                posAtual = (Posicao)pilha.Desempilhar().Clone();
                qtdCaminhos++;
            }
        }

        // "Pinta" por onde o cursor passou, seguindo o algoritmo
        private void Pintar(ref DataGridView dgvLab, int lin, int col)
        {
            if (listaCaminhos.Count == 0)
            {
                dgvLab[col, lin].Selected = true;
                System.Threading.Thread.Sleep(500);
                Application.DoEvents();
                dgvLab[col, lin].Selected = false;
            }
        }

        // Rosa dos Ventos
        private bool TemCaminho(ref Posicao posAtual, ref Posicao proxPosicao)
        {
            // Norte
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == VAZIO || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna] == SAIDA)
            {
                if (posAtual.OndeParou < 0)
                {
                    posAtual.OndeParou = 0;

                    proxPosicao.Linha -= 1;
                    return true;
                }
            }
            // Nordeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == VAZIO || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna + 1] == SAIDA)
            {
                if (posAtual.OndeParou < 1)
                {
                    posAtual.OndeParou = 1;

                    proxPosicao.Linha -= 1;
                    proxPosicao.Coluna += 1;
                    return true;
                }
            }
            // Leste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == VAZIO || this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna + 1] == SAIDA)
            {
                if (posAtual.OndeParou < 2)
                {
                    posAtual.OndeParou = 2;

                    proxPosicao.Coluna += 1;
                    return true;
                }
            }
            // Sudeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == VAZIO || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna + 1] == SAIDA)
            {
                if (posAtual.OndeParou < 3)
                {
                    posAtual.OndeParou = 3;

                    proxPosicao.Linha += 1;
                    proxPosicao.Coluna += 1;
                    return true;
                }
            }
            // Sul
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == VAZIO || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna] == SAIDA)
            {
                if (posAtual.OndeParou < 4)
                {
                    posAtual.OndeParou = 4;

                    proxPosicao.Linha += 1;
                    return true;
                }
            }
            // Sudoeste
            if (this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == VAZIO || this.labirinto.Matriz[posAtual.Linha + 1, posAtual.Coluna - 1] == SAIDA)
            {
                if (posAtual.OndeParou < 5)
                {
                    posAtual.OndeParou = 5;

                    proxPosicao.Linha += 1;
                    proxPosicao.Coluna -= 1;
                    return true;
                }
            }
            // Oeste
            if (this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == VAZIO || this.labirinto.Matriz[posAtual.Linha, posAtual.Coluna - 1] == SAIDA)
            {
                if (posAtual.OndeParou < 6)
                {
                    posAtual.OndeParou = 6;

                    proxPosicao.Coluna -= 1;
                    return true;
                }
            }
            // Noroeste
            if (this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == VAZIO || this.labirinto.Matriz[posAtual.Linha - 1, posAtual.Coluna - 1] == SAIDA)
            {
                if (posAtual.OndeParou < 7)
                {
                    posAtual.OndeParou = 7;

                    proxPosicao.Linha -= 1;
                    proxPosicao.Coluna -= 1;
                    return true;
                }
            }
            return false;
        }
    }
}