using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private static Dictionary<long, Cliente> ListaClientes;

        public ClienteService() { }

        public async Task<IEnumerable<Cliente>> BuscaTodosOsClientes()
        {
            return await Task.Run(()=> ListaClientes.Values.ToList());
        }

        public async Task<string> AdicionarCliente(Cliente cliente)
        {
            await Task.Run(()=> ListaClientes.Add(cliente.CPF, cliente));
            return "Cliente adicionado Com sucesso!";
        }

    }
}
