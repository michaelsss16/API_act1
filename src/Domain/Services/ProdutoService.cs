using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.DTO;

namespace Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _Repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _Repository = repository;
        }

        public async Task<IEnumerable<Produto>> BuscarTodosOsProdutos()
        {
            return await _Repository.Get();
        }

        public async Task<Produto> BuscarProdutoPorId(Guid guid)
        {
            return await _Repository.Get(guid);
        }

        public async Task<string> AdicionarProduto(ProdutoDTO produtodto)
        {
            Produto produto = new Produto() {Nome = produtodto.Nome,   Valor = produtodto.Valor, Descricao = produtodto.Descricao, Quantidade = produtodto.Quantidade, Id = Guid.NewGuid() };
            return await _Repository.Add(produto);
        }

    }
}
