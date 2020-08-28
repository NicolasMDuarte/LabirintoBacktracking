using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19168_19192_ED_Lab.Classes
{
    class Labirinto: ICloneable
    {
        private char[,] labirinto;
        int qtdLinhas;
        int qtdColunas;

        public Labirinto(int qtdLinhas, int qtdColunas)
        {
            this.qtdLinhas = qtdLinhas;
            this.qtdColunas = qtdColunas;
            labirinto = new char[qtdLinhas, qtdColunas];
        }

        public Labirinto(char[,] labirinto, int qtdLinhas, int qtdColunas)
        {
            this.qtdLinhas = qtdLinhas;
            this.qtdColunas = qtdColunas;
            this.labirinto = (char[,]) labirinto.Clone();
        }

        public Labirinto(Labirinto lab)
        {
            this.labirinto = (char[,])lab.labirinto.Clone();
            this.qtdLinhas = lab.qtdLinhas;
            this.qtdColunas = lab.qtdColunas;
        }

        public char[,] Matriz 
        { 
            get => labirinto;
        }

        public object Clone()
        {
            Labirinto ret = null;

            try
            {
                ret = new Labirinto(this);
            }
            catch (Exception)
            { }

            return ret;
        }
    }
}
