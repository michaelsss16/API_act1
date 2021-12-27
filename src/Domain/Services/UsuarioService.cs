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

        public async Task<IEnumerable<Usuario>> BuscarTodosOsUsuarios()
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
            try { await ValidarTodasAsRegras(usuariodto); }
            catch (Exception E) { return E.Message; }
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

        public async Task<bool> ValidarCadastro(UsuarioDTO usuariodto)
        {
            var Request = await _repository.Get();
            var Lista = Enumerable.ToList(Request);
            var Result1 = Lista.Exists(C => C.CPF == usuariodto.CPF);
            var Result2 = Lista.Exists(C => C.Email == usuariodto.Email);
            return (Result1 || Result2);
        }

        public async Task ValidarTodasAsRegras(UsuarioDTO usuariodto)
        {
            if (usuariodto == null) { throw new Exception("Não é possível adicionar usuário com campos vazios"); }
            if (!ValidarCPF(usuariodto.CPF)) { throw new Exception("O CPF informado não é válido"); }
            if (await ValidarCadastro(usuariodto)) { throw new Exception("Já existe cadastro de usuário com mesmo CPF ou Email"); }
        }


    }
}
