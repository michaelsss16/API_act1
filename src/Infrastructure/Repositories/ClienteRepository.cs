using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private static Dictionary<string, Cliente> ListaClientes = new Dictionary<string, Cliente>();

        public ClienteRepository() { }

        public async Task<IEnumerable<Cliente>> BuscarTodosOsClientes()
        {
            return await Task.Run(() => ListaClientes.Values.ToList());
        }

        public async Task<Cliente> BuscaClientePorCPF(string cpf)
        {
            return await Task.Run(() => ListaClientes.GetValueOrDefault(cpf));
        }

        public async Task<string> AdicionarCliente(Cliente cliente)
        {
            await Task.Run(() => ListaClientes.Add(cliente.CPF, cliente));
            return "Cliente adicionado com sucesso!";
        }
    }
}
