using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Services;
using Application.Interfaces;

namespace Application.AppServices
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoService _Service;

        public ProdutoAppService(IProdutoService service)
        {
            _Service = service;
        }

        public async Task<IEnumerable<Produto>> BuscarTodosOsProdutos()
        {
            return await _Service.BuscarTodosOsProdutos();
        }

        public async Task<Produto> BuscarProdutoPorId(Guid guid)
        {
            return await _Service.BuscarProdutoPorId(guid);
        }

        public async Task<string> AdicionarProduto(ProdutoDTO produto)
        {
            return await _Service.AdicionarProduto(produto);
        }
    }
}
