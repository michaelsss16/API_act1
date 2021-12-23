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
    public class VendaRepository : IVendaRepository
    {
        private readonly appContext _context;

        public VendaRepository(appContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venda>> Get()
        {
            return await _context.Vendas.ToListAsync();
        }

        public async Task<Venda> Get(Guid id)
        {
            return await _context.Vendas.FindAsync(id);
        }

        public async Task<string> Add(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return "Venda adicionada com sucesso!";
        }

    }
}
