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
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly appContext _context;

        public ProdutoRepository(appContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> Get()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> Get(Guid guid)
        {
            var produto = await _context.Produtos.FindAsync(guid);
            return produto;
        }

        public async Task<string> Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return "Produto adicionado com sucesso!";
        }

        public async Task<string> Update(Produto produto)
        {
            //var busca = await _context.Produtos.FindAsync(produto.Id);
            //_context.Produtos.Remove(busca);
//            await _context.SaveChangesAsync();

            //_context.Produtos.Add(produto);
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return "Produto atualizado com sucesso";
        }

    }
}
