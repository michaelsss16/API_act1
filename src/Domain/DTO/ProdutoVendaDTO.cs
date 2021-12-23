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

        public Guid  ProdutoId {   set; get;}

            public int Quantidade { get; set; }

        public Venda Venda{ get; set; }
        public Produto Produto { set; get; }
        public Guid VendaId { set; get; }
    }
}
