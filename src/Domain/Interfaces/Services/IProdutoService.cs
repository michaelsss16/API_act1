﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.DTO;

namespace Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        public Task<IEnumerable<Produto>> BuscarTodosOsProdutos();

        public Task<Produto> BuscarProdutoPorId(Guid guid);

        public Task<string> AdicionarProduto(ProdutoDTO produto);
    }
}