using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apBalanceamento
{
    public class PilhaLista1<Dado> : IStack<Dado> where Dado: IComparable<Dado>
    {
        NoLista<Dado> topo;
        int tamanho;

        public PilhaLista1()         // construtor
        {
            topo = null;
            tamanho = 0;
        }

        public int Tamanho { get => tamanho; }

        public bool EstaVazia { get => topo == null; }

        public void Empilhar(Dado o)
        {
            // Instancia um nó, coloca o Dado o nele e o liga ao antigo topo da pilha
            NoLista<Dado> novoNo = new NoLista<Dado>(o, topo);
            topo = novoNo;          // topo passa a apontar o novo nó
            tamanho++;              // atualiza número de elementos na pilha
        }

        public Dado OTopo()
        {
            if (EstaVazia)
                throw new PilhaVaziaException("Underflow da pilha");
            return topo.Info;
        }

        public Dado Desempilhar()
        {
            if (EstaVazia)
                throw new PilhaVaziaException("Underflow da pilha");
            Dado o = topo.Info;     // obtém o objeto do topo
            topo = topo.Prox;       // avança topo para o nó seguinte
            tamanho--;          // atualiza número de elementos na pilha
            return o;           // devolve o objeto que estava no topo
        }
    }

}
