using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly appContext _context;

        public ClienteRepository(appContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosOsClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> BuscaClientePorCPF(string cpf)
        {
            var result = new Cliente();
            var lista = await _context.Clientes.ToListAsync();
            foreach (Cliente c in lista)
            {
                if (c.CPF == cpf)
                {
                    result = c;
                }
            }
            return result;
        }

        public async Task<string> AdicionarCliente(Cliente cliente)
        {
            cliente.DataDeInsercao = DateTime.Now;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return "Cliente adicionado com sucesso!";
        }
    }
}
