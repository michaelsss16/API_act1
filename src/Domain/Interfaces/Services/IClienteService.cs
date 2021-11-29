using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        public Task<IEnumerable<Cliente>> BuscaTodosOsClientes();
        
        //public Task<Cliente> BuscaClientePorCPF();
        
        public Task<string> AdicionarCliente(Cliente cliente);
    }
}
