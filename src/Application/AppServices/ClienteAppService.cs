using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Services;
using Application.Interfaces;

namespace Application.AppServices
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteService _Service;

        public ClienteAppService(IClienteService service)
        {
            _Service = service;
        }

        public async Task<string> ValidarEAdicionarCliente(Cliente request)
        {
            try
            {
                await _Service.ValidarTodasAsRegras(request);
            }
            catch (Exception E)
            {
                return E.Message;
            }
            return await _Service.CadastrarCliente(request);
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosOsClientes()
        {
            return await _Service.BuscarTodosOsClientes();
        }

        public async Task<Cliente> BuscarClientePorCPF(string cpf)
        {
            return await _Service.BuscarClientePorCPF(cpf);
        }
    }
}
