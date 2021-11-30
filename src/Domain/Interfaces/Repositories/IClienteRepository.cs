using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Cliente>> BuscarTodosOsClientes();

        public Task<Cliente> BuscaClientePorCPF(string cpf);

        public Task<string> AdicionarCliente(Cliente cliente);
    }
}
