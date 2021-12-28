using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<IEnumerable<UsuarioGetDTO>> BuscarTodosOsUsuarios()
        {
            var lista = await _repository.Get();
            var retorno = new List<UsuarioGetDTO>();
            foreach (var usuario in lista)
            {
var u  = new UsuarioGetDTO(){ Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email, Tipo = usuario.Tipo, CPF = usuario.CPF };
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
            usuariodto.CPF = usuariodto.CPF.Trim();
            usuariodto.CPF = usuariodto.CPF.Replace(".", "").Replace("-", "");
            usuariodto.Tipo = usuariodto.Tipo.Trim();

            try { await ValidarTodasAsRegras(usuariodto); }
            catch (Exception E) { return E.Message; }
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
            if (usuariodto == null) { throw new Exception("Não é possível adicionar usuário com campos vazios"); }
            if (!ValidarCPF(usuariodto.CPF)) { throw new Exception("O CPF informado não é válido"); }
            if (await ValidarCadastro(usuariodto)) { throw new Exception("Já existe cadastro de usuário com mesmo CPF ou Email"); }
            if (ValidarTipoDeUsuario(usuariodto)) { throw new Exception("O tipo de usuário informado não é válido"); }
        }

        public static string Encriptar(string mensagem)
        {
            try
            {
                string textToEncrypt = mensagem;
                string ToReturn = "";
                string publickey = "12345678";
                string secretkey = "87654321";
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        static string Descriptar(string mensagem)
        {
            try
            {
                string textToDecrypt = mensagem;
                string ToReturn = "";
                string publickey = "12345678";
                string secretkey = "87654321";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }

    }
}
