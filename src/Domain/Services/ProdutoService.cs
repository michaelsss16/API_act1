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
    public class ProdutoService :  IProdutoService
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

        public async Task<List<Produto>> BuscarListaDeProdutosPorId(List<Guid> listaIds)
        {
            var Result = new List<Produto>();
            foreach (Guid item in listaIds)
            {
                Result.Add(await _Repository.Get(item));
            }
            return Result;
        }

        public async Task<string> AdicionarProduto(ProdutoDTO produtodto)
        {
            Produto produto = new Produto() { Nome = produtodto.Nome, Valor = produtodto.Valor, Descricao = produtodto.Descricao, Quantidade = produtodto.Quantidade, Id = Guid.NewGuid(), DataDeInsercao=DateTime.Now };
            return await _Repository.Add(produto);
        }

        public async Task<string> AtualizarProduto(Produto produto)
        {
            return await _Repository.Update(produto);
        }

        public async Task ValidarVenda(List<ProdutoVendaDTO> produtos)
        {
            foreach (ProdutoVendaDTO item in produtos)
            {
                //item.Id = Guid.NewGuid();
                var produto = await BuscarProdutoPorId(item.ProdutoId);
                if (produto == null) { throw new Exception("Não existe produto com o id informado: " + item.ProdutoId.ToString()); }
                if (item.Quantidade > produto.Quantidade) { throw new Exception("Não existe quantidade suficiente de produtos no estoque para o id: " + item.ProdutoId.ToString()); }
                if (item.Quantidade <= 0) { throw new Exception("A quantidade informada não é válida no produto com id: " + item.ProdutoId.ToString()); }
            }
        }

        public async Task<string> AtualizarListaDeProdutos(List<ProdutoVendaDTO> produtos)
        {
            foreach (ProdutoVendaDTO item in produtos)
            {
                var produto = await BuscarProdutoPorId(item.ProdutoId);
                produto.Quantidade -= item.Quantidade;
                await AtualizarProduto(produto);
            }
            return "Produtos atualizados com sucesso";
        }
    }
}
