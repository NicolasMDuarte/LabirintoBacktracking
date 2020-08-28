using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19168_19192_ED_Lab.Classes
{
    class Posicao : IComparable<Posicao>
    {
        int[] coordenadas;

        public Posicao()
        {
            coordenadas = new int[2];
        }

        public Posicao(int linha, int coluna)
        {
            coordenadas = new int[2] { linha, coluna };
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

        public int CompareTo(Posicao pos)
        {
            if (this.coordenadas[0] + this.coordenadas[1] == pos.coordenadas[0] + pos.coordenadas[1])
                return 0;

            return this.coordenadas[0] + this.coordenadas[1] > pos.coordenadas[0] + pos.coordenadas[1] ? 1 : -1;
        }
    }
}
