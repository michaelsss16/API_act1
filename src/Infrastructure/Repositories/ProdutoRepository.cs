using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private Dictionary<Guid, Produto> ListaProdutos = new Dictionary<Guid, Produto>();

        public async Task<IEnumerable<Produto>> Get()
        {
            return await Task.Run(() => ListaProdutos.Values.ToList());
        }

        public async Task<Produto> Get(Guid guid)
        {
            return await Task.Run(() => ListaProdutos.GetValueOrDefault(guid));
        }

        public async Task<string> Add(Produto produto)
        {
            await Task.Run(() => ListaProdutos.Add(produto.Id, produto));
            return "Produto adicionado com sucesso!";
        }

        public async Task<string> Update(Produto produto)
        {
            await Task.Run(()=>ListaProdutos.Remove(produto.Id));
            await Task.Run(()=>ListaProdutos.Add(produto.Id, produto));
            return "Produto atualizado com sucesso";
        }
    }
}
