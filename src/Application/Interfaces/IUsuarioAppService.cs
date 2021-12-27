using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioAppService
    {
        public Task<IEnumerable<Usuario>> BuscarrTodosOsUsuarios();
        public Task<Usuario> BuscarUsuarioPorId(Guid id);
        public Task<string> AdicionarUsuario(UsuarioDTO usuariodto);
        public Task<string> RemoverUsuario(Usuario usuario);
    }
}
