using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioService _service;

        public UsuarioAppService(IUsuarioService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<UsuarioGetDTO>> BuscarrTodosOsUsuarios()
        {
            return await _service.BuscarTodosOsUsuarios();
        }

        public async Task<UsuarioGetDTO> BuscarUsuarioPorId(Guid id)
        {
            return await _service.BuscarUsuarioPorId(id);
        }

        public async Task<string> AdicionarUsuario(UsuarioDTO usuariodto)
        {
            return await _service.AdicionarUsuario(usuariodto);
        }

        public async Task<string> RemoverUsuario(Usuario usuario)
        {
            return await _service.RemoverUsuario(usuario);
        }
    }
}
