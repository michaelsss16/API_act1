using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        public Task<string> AdicionarCliente(IClienteService cliente);
    }
}
