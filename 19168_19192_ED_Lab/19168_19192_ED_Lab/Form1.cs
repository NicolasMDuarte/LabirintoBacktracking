using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19168_19192_ED_Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            string arquivo = "";
            string linha = "";

            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                arquivo = dlgAbrir.FileName;
                StreamReader leitor = new StreamReader(arquivo);
                while(!leitor.EndOfStream)
                {
                    linha = leitor.ReadLine();
                    //paradas muito legais que o Chico quer que a gente faça *carinha feliz*, ou seja, 
                    //penetrar deliciosamente todos os dados do arquivo texto em uma bela matriz
                }
            }
        }

        private void btnEncontrar_Click(object sender, EventArgs e)
        {
            //ENCONTRA LOGO OS CAMINHOS, PÔ! *carinha triste*
        }
    }
}
