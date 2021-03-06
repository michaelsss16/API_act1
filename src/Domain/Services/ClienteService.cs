using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Utils;

namespace Domain.Services
{
    public class ClienteService : Util, IClienteService
    {
        private readonly IClienteRepository _Repository;
        public ClienteService(IClienteRepository repository)
        {
            _Repository = repository;
        }

        public async Task<bool> ValidarCadastro(Cliente cliente)
        {
            var Request = await _Repository.BuscarTodosOsClientes();
            var Lista = Enumerable.ToList(Request);
            var Result1 = Lista.Exists(C => C.CPF == cliente.CPF);
            var Result2 = Lista.Exists(C => C.Email == cliente.Email);
            return (Result1 || Result2);
        }

        public async Task ValidarTodasAsRegras(Cliente cliente)
        {
            cliente.CPF = Utils.Util.FormatarCPF(cliente.CPF); 
            if (!(ValidarCPF(cliente.CPF)))
            {
                throw new InvalidOperationException("O CPF informado não é válido");
            }
            if (await ValidarCadastro(cliente))
            {
                throw new InvalidOperationException("Já existe cadastro com mesmo CPF ou Email");
            }
        }// fim do método validar todas as regras

        public async Task<string> CadastrarCliente(Cliente cliente)
        {
            cliente.CPF = Util.FormatarCPF(cliente.CPF);
            return await _Repository.AdicionarCliente(cliente);
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosOsClientes()
        {
            return await _Repository.BuscarTodosOsClientes();
        }

        public async Task<Cliente> BuscarClientePorCPF(string cpf)
        {
            cpf = Util.FormatarCPF(cpf);
            var cliente = await _Repository.BuscaClientePorCPF(cpf);
            if (cliente == null) { throw new Exception("Não existe cliente cadastrado com o CPF informado"); }
            return cliente;
        }

    }
}
