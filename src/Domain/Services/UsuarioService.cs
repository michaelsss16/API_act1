using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UsuarioService : ValidacoesService, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> BuscarrTodosOsUsuarios()
        {
            return await _repository.Get();
        }

        public async Task<Usuario> BuscarUsuarioPorId(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<string> AdicionarUsuario(UsuarioDTO usuariodto)
        {
            usuariodto.CPF = usuariodto.CPF.Trim();
            usuariodto.CPF = usuariodto.CPF.Replace(".", "").Replace("-", "");
            ValidarCPF(usuariodto.CPF);
            var usuario = new Usuario() { Id = Guid.NewGuid(), Nome = usuariodto.Nome, Email = usuariodto.Email, Tipo = (Entities.UserType)usuariodto.Tipo, Senha = usuariodto.Senha, CPF = usuariodto.CPF };
            // Encriptar a senha
            return await _repository.Add(usuario);
        }

        public async Task<string> AtualizarUsuario(Usuario usuario)
        {
            return await _repository.Update(usuario);
        }

        public async Task<string> RemoverUsuario(Usuario usuario)
        {
            return await _repository.Delete(usuario);
        }



    }
}
