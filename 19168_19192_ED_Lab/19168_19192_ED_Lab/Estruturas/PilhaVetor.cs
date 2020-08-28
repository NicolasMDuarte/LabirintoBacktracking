using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apBalanceamento
{
  class PilhaVetor<Dado> : IStack<Dado>
  {
    Dado[] p;  // área de armazenamento
    int topo;  // índice da última posição usada do vetor que representa a pilha
    int posicoes;
    public Dado Desempilhar()
    {
      if (EstaVazia)
        throw new PilhaVaziaException("Underflow. Pilha esvaziou.");
      Dado elemento = p[topo--];
      return elemento;
    }

    public void Empilhar(Dado elemento)
    {
      if (Tamanho >= posicoes)
        throw new PilhaCheiaException("Overflow. Pilha transbordou.");
      p[++topo] = elemento;
    }

    public bool EstaVazia {  get => topo < 0; }

    public Dado OTopo()
    {
      if (EstaVazia)
        throw new PilhaVaziaException("Underflow. Pilha esvaziou");
      return p[topo];
    }

    public int Tamanho { get => topo + 1; }

    public PilhaVetor(int posic)
    {
      p = new Dado[posic];
      posicoes = posic;
      topo = -1;
    }

    public PilhaVetor() : this(500)
    { }

    public void Exibir(DataGridView dgv)
    {
      for (int j = 0; j < 20; j++)
        dgv[j, 0].Value = "";

      PilhaVetor<Dado> auxiliar = new PilhaVetor<Dado>();
      int i = 0;
      while (!this.EstaVazia)
      {
        dgv[i++, 0].Value = this.OTopo();
        Thread.Sleep(300);
        Application.DoEvents();
        auxiliar.Empilhar(this.Desempilhar());
      }

      while (!auxiliar.EstaVazia)
        this.Empilhar(auxiliar.Desempilhar());
    }
  }
}
