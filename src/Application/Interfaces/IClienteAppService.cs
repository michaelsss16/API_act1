using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClienteAppService
    {
        public Task<string> ValidarEAdicionarCliente(Cliente cliente);

        public Task<IEnumerable<Cliente>> BuscarTodosOsClientes();

        public Task<Cliente> BuscarClientePorCPF(string cpf);
    }
}
