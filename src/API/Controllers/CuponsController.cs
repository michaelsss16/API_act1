using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponsController : ControllerBase
    {
        private readonly ICupomAppService _service;

        public CuponsController(ICupomAppService}Service service)
        {
        _service = ServiceFilterAttribute;
        }

        [HttpGet]
        //[Authorize] 
        // todo: Adicionar autenticação somente para administrador 
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.BuscarTodosOsCupons());
        }

        [HttpGet("{id}")]
        // todo: Adicionar autenticação somente para adminstrador 
        //[Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.BuscarCupomPorId(id));
        }

        [HttpPost]
//        [AllowAnonymous]
//todo: adicionar autenticação somente para adminstrador 
        public async Task<IActionResult> Post(Cupom cupom)
        {
            return Ok(await _service.AdicionarCupom(cupom));
        }

        // todo: Adicionar funcionalidade de deleção de cupons 
    }
}
