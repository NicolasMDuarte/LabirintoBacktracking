using Supermercado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apBalanceamento;

namespace AppFila
{
    public class FilaVetor<Tipo> : IQueue<Tipo> 
    {
        public static int MAXIMO = 500; // tamanho default do vetor F
        private int posicoes; // tamanho dado pela aplicação
        private Tipo[] F; // vetor de objetos genéricos, com tamanho genérico,
                          // usado como área de armazenamento
        private int inicio = 0, // índice do início da fila
                    fim = 0;    // índice do fim da fila
        public FilaVetor() : this(MAXIMO) // construtor que utiliza o default MAXIMO
        {                                 // chama o método construtor com parâmetro
        }                                 // utilizando o polimorfismo do construtor
        public FilaVetor(int posic) // construtor polimórfico
        {
          posicoes = posic;         // armazena o tamanho físico do vetor
          F = new Tipo[posicoes];   // F é um vetor de Tipo; cria um
        }                           // vetor F com o tamanho indicado
        public int tamanho() { // devolve o número de posições em uso
          return (posicoes - inicio + fim) % posicoes;
        }
        public bool estaVazia() { // devolve true se fila está vazia,
          return (inicio == fim); // e false caso contrário
        }
        public Tipo oInicio()
        {
            if (estaVazia())
                throw new FilaVaziaException(
                    "Esvaziamento (underflow) da fila");
            Tipo o = F[inicio]; // devolve o objeto do início da fila
            return o; // sem retirá-lo da fila
        }
        public Tipo oFim()
        {
            Tipo o;
            if (estaVazia())
                throw new FilaVaziaException("Underflow da fila");
            if (fim == 0)
                o = F[posicoes-1]; // devolve o objeto do final da fila
            else // sem retirá-lo da fila
                o = F[fim-1];
            return o;
        }
        public void enfileirar(Tipo o) 
        {
            if (tamanho() == posicoes - 1)
                throw new FilaCheiaException("Fila cheia (overflow)");
            F[fim] = o;                 // inclui elemento na primeira posição livre
            fim = (fim + 1) % posicoes; // calcula próxima posição livre
        }
        public Tipo retirar() 
        {
            Tipo o;
            if (estaVazia())
                throw new FilaVaziaException("Underflow da fila");
            o = F[inicio];      // copia o elemento inicial da fila
            F[inicio] = default(Tipo);   // libera memória
            inicio = (inicio +1) % posicoes; // calcula novo inicio da fila
            return o;                        // devolve elemento inicial
        }
    }
}
