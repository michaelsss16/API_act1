using API.Services;
using Application.Interfaces;
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
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioAppService _service;

        public LoginController(IUsuarioAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Faça a chamada enviando as credenciais de login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>>  Get(Login login)
        {
            try
            {
                var user = await _service.EncontrarOcorrenciaPorCredencial(login);
                var token = TokenService.GenerateToken(user);
                return Ok(
                    new { User = user, Token = token}
                    );
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
