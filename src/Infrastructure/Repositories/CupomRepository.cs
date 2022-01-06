using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CupomRepository : ICupomRepository
    {
        private readonly appContext _context;

        public CupomRepository(appContext context)
        {
            _context = context;
        }

        public async Task<List<Cupom>> Get()
        {
            return await _context.Cupons.ToListAsync();
        }
        public async Task<Cupom> Get(Guid guid)
        {
            return await _context.Cupons.FindAsync(guid);
        }

        public async Task<string> Add(Cupom cupom)
        {
            try
            {
                _context.Cupons.Add(cupom);
                await _context.SaveChangesAsync();
                return "Cupom adicionado com sucesso";
            }
            catch (Exception e) {
                return ("Erro ao adicionar o cupom\n" + e.Message);
            }   

        }


    }
}
