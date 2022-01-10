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
            var lista = await _context.Vendas.ToListAsync();
            foreach (var item in lista)
            {
                _context.Entry(item).Collection(p => p.ListaProdutos).Load();
            }
            return lista;
        }

        public async Task<Venda> Get(Guid id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            _context.Entry(venda).Collection(p => p.ListaProdutos).Load();
            return venda;
        }

        public async Task<string> Add(Venda venda)
        {
            venda.DataDeInsercao = DateTime.Now;
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return "Venda adicionada com sucesso!";
        }

    }
}
