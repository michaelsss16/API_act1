﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Domain.Entities
{
    public class Venda
    {
        public DateTime DataDeInsercao { get; set; }
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public List<ProdutoVendaDTO> ListaProdutos { get; set; }
        public double Valor { set; get; }

        //public ProdutoVendaDTO ProdutoVendaDTO { get; set; }
    }
}
