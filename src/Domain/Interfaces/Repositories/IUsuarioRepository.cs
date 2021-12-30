using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<IEnumerable<Usuario>> Get();
        public Task<Usuario> Get(Guid id);
        public Task<string> Add(Usuario usuario);
        public Task<string> Update(Usuario usuario);
        public Task<string> Delete(Usuario usuario);
    }
}
