using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Cliente>> BuscarTodosOsClientes();

        public Task<string> AdicionarCliente(Cliente cliente);
    }
}
