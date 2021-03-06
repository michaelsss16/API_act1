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
        public Task<IEnumerable<UsuarioGetDTO>> BuscarrTodosOsUsuarios();
        public Task<UsuarioGetDTO> BuscarUsuarioPorId(Guid id);
        public Task<string> AdicionarUsuario(UsuarioDTO usuariodto);
        public Task<string> RemoverUsuario(Usuario usuario);
        public Task<UsuarioToken> EncontrarOcorrenciaPorCredencial(Login login);
    }
}
