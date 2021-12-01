using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> Get();

        public Task<Produto> Get(Guid guid);

        public Task<string> Add(Produto produto);
    }
}
