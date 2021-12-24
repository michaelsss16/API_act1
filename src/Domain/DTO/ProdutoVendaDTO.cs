using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ProdutoVendaDTO
    {
        public Guid Id { get; set; }
            public int Quantidade { get; set; }

        public Venda Venda{ get; set; }
        public Guid VendaId { set; get; }
        public Produto Produto { set; get; }
        public Guid  ProdutoId {   set; get;}
    }
}
