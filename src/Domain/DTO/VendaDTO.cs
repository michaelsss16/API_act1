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
        public List<ProdutoVendaDTO> ListaProdutos { get; set; }
        public Guid CupomId { get; set; }
    }
}
