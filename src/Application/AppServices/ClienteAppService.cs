using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Application.Interfaces;

namespace Application.AppServices
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteService _Service;
        private readonly IClienteRepository _Repository;

        public ClienteAppService(IClienteService service, IClienteRepository repository)
        {
            _Service = service;
            _Repository = repository;
        }

        public async Task<string> ValidarEAdicionarCliente(Cliente request)
        {
            request.CPF = request.CPF.Trim();
            request.CPF = request.CPF.Replace(".", "").Replace("-", "");

            if (!(await _Service.ValidarCPF(request)))
            {
                return "O CPF informado não é válido.";
            }
            if (await _Service.ValidarCadastro(request))
            {
                return "Já existe cadastro com mesmo CPF ou Email.";
            }
            var Resultado = await Task.Run(() => _Repository.AdicionarCliente(request));
            return "Cliente adicionado com sucesso!";
        }
        public async Task<IEnumerable<Cliente>> BuscarTodosOsClientes()
        {
            return await _Repository.BuscarTodosOsClientes();
        }

        public async Task<Cliente> BuscarClientePorCPF(string cpf)
        {
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            return await _Repository.BuscaClientePorCPF(cpf);
        }
    }
}
