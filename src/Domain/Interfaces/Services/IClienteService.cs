using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        public Task<bool> ValidarCadastro(Cliente cliente);

        public Task<bool> ValidarCPF(Cliente cliente);
    }
}
