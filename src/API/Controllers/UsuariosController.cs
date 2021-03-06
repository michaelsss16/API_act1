using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _service;
        public UsuariosController(IUsuarioAppService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize] 
        // todo: Retornar a autenticação 
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.BuscarrTodosOsUsuarios());
        }

        [HttpGet("{id}")]
        // Retornar a autenticação 
        //[Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.BuscarUsuarioPorId(id));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UsuarioDTO usuariodto)
        {
            return Ok(await _service.AdicionarUsuario(usuariodto));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Usuario usuario)
        {
            return Ok(await _service.RemoverUsuario(usuario));
        }
    }
}
