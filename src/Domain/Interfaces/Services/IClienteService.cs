using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        public Task<bool> ValidarCadastro(Cliente cliente);

        public bool ValidarCPF(string cpf );

        public Task ValidarTodasAsRegras(Cliente cliente);

        public Task<string> CadastrarCliente(Cliente cliente);

        public Task<Cliente> BuscarClientePorCPF(string cpf);

        public Task<IEnumerable<Cliente>> BuscarTodosOsClientes();
    }
}
