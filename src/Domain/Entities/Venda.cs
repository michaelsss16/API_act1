using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Venda
    {
        public Guid Id { get; set; }

        public string CPF { get; set; }

        public List<Guid> Guids { get; set; }

        public List<int> Quantidades { get; set; }
    }
}
