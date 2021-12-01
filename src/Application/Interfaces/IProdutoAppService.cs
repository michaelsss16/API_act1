using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IProdutoAppService
    {
        public Task<IEnumerable<Produto>> BuscarTodosOsProdutos();

        public Task<Produto> BuscarProdutoPorId(Guid guid);

        public Task<string> AdicionarProduto(ProdutoDTO produto);
    }
}
