using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado
{
  interface IQueue<Tipo>
  {
    void enfileirar(Tipo elemento);   // inclui objeto “elemento”
    Tipo retirar();           // devolve objeto do início e o 
                              // retira da fila 
    Tipo oInicio();           // devolve objeto do início
                              // sem retirá-lo da fila 
    Tipo oFim();                // devolve objeto do fim
                                // sem retirá-lo da fila 
    int tamanho();        // devolve número de elementos da fila
    bool estaVazia();			// informa se a fila está vazia ou não
  }
}
