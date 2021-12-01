using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double Valor { set; get; }

        public int Quantidade { set; get; }
    }
}
