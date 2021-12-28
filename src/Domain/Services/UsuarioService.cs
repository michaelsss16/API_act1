using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UsuarioService : Util, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioGetDTO>> BuscarTodosOsUsuarios()
        {
            var lista = await _repository.Get();
            var retorno = new List<UsuarioGetDTO>();
            foreach (var usuario in lista)
            {
                var u = new UsuarioGetDTO() { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email, Tipo = usuario.Tipo, CPF = usuario.CPF };
                retorno.Add(u);
            }
            return retorno;
        }

        public async Task<UsuarioGetDTO> BuscarUsuarioPorId(Guid id)
        {
            var usuario = await _repository.Get(id);
            var usuariogetdto = new UsuarioGetDTO() { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email, Tipo = usuario.Tipo, CPF = usuario.CPF };
            return usuariogetdto;
        }

        public async Task<string> AdicionarUsuario(UsuarioDTO usuariodto)
        {
            usuariodto = FormatarUsuarioDTO(usuariodto);
            await ValidarTodasAsRegras(usuariodto);
            usuariodto.Senha = Encriptar(usuariodto.Senha);
            var usuario = new Usuario() { Id = Guid.NewGuid(), Nome = usuariodto.Nome, Email = usuariodto.Email, Tipo = usuariodto.Tipo, Senha = usuariodto.Senha, CPF = usuariodto.CPF };
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

        public bool ValidarTipoDeUsuario(UsuarioDTO usuariodto)
        {
            if ((usuariodto.Tipo == "cliente") || (usuariodto.Tipo == "administrador"))
            {
                return false;
            }
            return true;
        }

        public async Task ValidarTodasAsRegras(UsuarioDTO usuariodto)
        {
            if (usuariodto == null) { throw new InvalidOperationException("Não é possível adicionar usuário com campos vazios"); }
            if (!ValidarCPF(usuariodto.CPF)) { throw new InvalidOperationException("O CPF informado não é válido"); }
            if (await ValidarCadastro(usuariodto)) { throw new InvalidOperationException("Já existe cadastro de usuário com mesmo CPF ou Email"); }
            if (ValidarTipoDeUsuario(usuariodto)) { throw new InvalidOperationException("O tipo de usuário informado não é válido"); }
        }

        public UsuarioDTO FormatarUsuarioDTO(UsuarioDTO usuariodto)
        {
            if (usuariodto.CPF != null)
            {
                usuariodto.CPF = usuariodto.CPF.Trim();
                usuariodto.CPF = usuariodto.CPF.Replace(".", "").Replace("-", "");
            }
            if (usuariodto.Tipo != null) { usuariodto.Tipo = usuariodto.Tipo.Trim(); }
            return usuariodto;
        }

    }
}
