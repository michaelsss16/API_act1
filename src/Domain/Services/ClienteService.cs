using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;


namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _Repository;
        public ClienteService(IClienteRepository repository) {
            _Repository = repository;
        }
        
        public async Task<bool>ValidarCadastro(Cliente cliente) {
            var lista = await _Repository.BuscarTodosOsClientes();
            return true;
        }
    }
}
