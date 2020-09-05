using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19168_19192_ED_Lab.Classes
{
    class Posicao : IComparable<Posicao>, ICloneable
    {
        private int[] coordenadas;
        private int ondeParou;

        public Posicao()
        {
            ondeParou = -1;
            coordenadas = new int[2]; //vetor de 2 posições
        }

        public Posicao(int linha, int coluna)
        {
            ondeParou = -1;
            coordenadas = new int[2] { linha, coluna }; //atribui a linha e a coluna à posição
        }

        public Posicao(int linha, int coluna, int ondeParou)
        {
            this.ondeParou = ondeParou;
            coordenadas = new int[2] { linha, coluna }; //atribui a linha e a coluna à posição
        }

        public int[] Coordenadas { get => coordenadas; set => coordenadas = value; }
        public int Linha
        {
            get => coordenadas[0];
            set => coordenadas[0] = value;
        }
        public int Coluna
        {
            get => coordenadas[1];
            set => coordenadas[1] = value;
        }
        public int OndeParou { get => ondeParou; set => ondeParou = value; }

        public object Clone()
        {
            return new Posicao(this.Linha, this.Coluna, this.ondeParou);
        }

        public int CompareTo(Posicao pos) //exigência na solucionadora
        {
            if (this.coordenadas[0] + this.coordenadas[1] == pos.coordenadas[0] + pos.coordenadas[1])
                return 0;

            return this.coordenadas[0] + this.coordenadas[1] > pos.coordenadas[0] + pos.coordenadas[1] ? 1 : -1;
        }
    }
}
