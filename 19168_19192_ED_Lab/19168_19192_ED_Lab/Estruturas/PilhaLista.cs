using System;
using System.Windows.Forms;
using System.Threading;

namespace apBalanceamento
{
    class PilhaLista<Dado> : ListaSimples<Dado>, ICloneable, IStack<Dado>
                             where Dado : IComparable<Dado>
    {
        public Dado Desempilhar()
        {
            if (EstaVazia)
                throw new PilhaVaziaException("pilha vazia!");

            Dado valor = base.Primeiro.Info;

            NoLista<Dado> pri = base.Primeiro;
            NoLista<Dado> ant = null;
            base.RemoverNo(ref pri, ref ant);
            return valor;
        }

        public void Empilhar(Dado elemento)
        {
            base.InserirAntesDoInicio
                  (
                    new NoLista<Dado>(elemento, null)
                  );
        }

        new public bool EstaVazia
        {
            get => base.EstaVazia;
        }

        public Dado OTopo()
        {
            if (EstaVazia)
                throw new PilhaVaziaException("pilha vazia!");

            return base.Primeiro.Info;
        }

        public int Tamanho { get => base.QuantosNos; }


        public void Exibir(DataGridView dgv)
        {
            for (int j = 0; j < 20; j++)
                dgv[j, 0].Value = "";

            var auxiliar = new PilhaLista<Dado>();
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


        // Método para criar cópias das pilhas usadas para o funcionamento do backtracking
        public PilhaLista<Dado> Copia()
        {
            var copia = new PilhaLista<Dado>();
            Dado[] aux = new Dado[this.Tamanho];

            for (int i = 0; !this.EstaVazia; i++)
                aux[i] = this.Desempilhar();

            for (int i = aux.Length - 1; i >= 0; i--)
            {
                this.Empilhar(aux[i]);
                copia.Empilhar(aux[i]);
            }

            // Inverte pois o algoritmo acima acaba invertendo a pilha, o que não é desejado.
            copia.Inverter();
            return copia;
        }
    }
}
