using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        public Task<IEnumerable<UsuarioGetDTO>> BuscarTodosOsUsuarios();
        public Task<UsuarioGetDTO> BuscarUsuarioPorId(Guid id);
        public Task<string> AdicionarUsuario(UsuarioDTO usuariodto);
        public Task<string> AtualizarUsuario(Usuario usuario);
        public Task<string> RemoverUsuario(Usuario usuario);
        public bool ValidarCPF(string cpf);
        public Task<bool> ValidarCadastro(UsuarioDTO usuariodto);
        public bool ValidarTipoDeUsuario(UsuarioDTO usuariodto);
        public Task ValidarTodasAsRegras(UsuarioDTO usuariodto);
        public UsuarioDTO FormatarUsuarioDTO(UsuarioDTO usuariodto);
        public Task<UsuarioToken> EncontrarOcorrenciaPorCredencial(Login login);
    }
}