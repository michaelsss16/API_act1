using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cupom
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public double  Porcentagem{ get; set; }
        public DateTime DataDeInsercao { get; set; }
    }
}
