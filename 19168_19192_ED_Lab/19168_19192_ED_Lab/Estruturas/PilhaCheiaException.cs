using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apBalanceamento
{
  class PilhaCheiaException : Exception
  {
    public PilhaCheiaException(string mensagem) : base(mensagem)
    {
    }
  }
}
