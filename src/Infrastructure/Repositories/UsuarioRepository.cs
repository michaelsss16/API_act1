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
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly appContext _context;

        public UsuarioRepository(appContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> Get(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<string> Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return "Usuário adicionado com sucesso";
        }

        public async Task<string> Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return "Usuário atualizado com sucesso";
        }

        public async Task<string>Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return "Usuário excluído com sucesso";
        }


    }
}
