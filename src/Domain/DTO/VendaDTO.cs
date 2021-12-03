using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class VendaDTO
    {
        public string CPF { get; set; }

        public List<Guid> Guids { get; set; }

        public List<int> Quantidades { get; set; }
    }
}
