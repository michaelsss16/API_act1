using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IVendaRepository
    {
        public Task<IEnumerable<Venda>> Get();

        public Task<Venda> Get(Guid id);

        public Task<string> Add(Venda venda);
    }
}
