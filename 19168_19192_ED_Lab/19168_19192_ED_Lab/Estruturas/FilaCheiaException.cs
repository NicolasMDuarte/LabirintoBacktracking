using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apBalanceamento
{
  class FilaCheiaException : Exception
  {
    public FilaCheiaException(string mensagem) : base(mensagem)
    {
    }
  }
}
