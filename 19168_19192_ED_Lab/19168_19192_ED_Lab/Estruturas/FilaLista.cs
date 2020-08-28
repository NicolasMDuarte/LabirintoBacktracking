using apBalanceamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado
{
  class FilaLista<Tipo> : ListaSimples<Tipo>, IQueue<Tipo>
    where Tipo : IComparable<Tipo>
  {
    public FilaLista() : base()
    { }

    public void enfileirar(Tipo elemento)
    {
      NoLista<Tipo> novoNo = new NoLista<Tipo>(elemento, null);
      base.InserirAposFim(novoNo); // inclui ao final da lista herdada
    }

    public Tipo oFim()
    {
      if (estaVazia())
        throw new FilaVaziaException("Fila vazia");

      Tipo o = base.Ultimo.Info; // acessa o 1o objeto genérico
      return o;
    }

    public Tipo oInicio()
    {
      if (estaVazia())
        throw new FilaVaziaException("Fila vazia");
      Tipo o = base.Primeiro.Info; // acessa o 1o objeto genérico
      return o;
    }

    public Tipo retirar()
    {
      if (!estaVazia())
      {
        Tipo elemento = base.Primeiro.Info;
        NoLista<Tipo> pri = base.Primeiro;
        NoLista<Tipo> ant = null;
        base.RemoverNo(ref pri, ref ant);
        return elemento;
      }
      throw new FilaVaziaException("Fila vazia");
    }

    public int tamanho()
    {
      return base.QuantosNos;
    }

    public bool estaVazia()
    {
      return base.EstaVazia;
    }
  }
}
