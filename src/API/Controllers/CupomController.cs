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
    public class CupomController : ControllerBase
    {
        public readonly ICupomAppService _service;

        public CupomController(ICupomAppService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize]
        //Todo: Retornar a autenticação 
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.BuscarTodosOsCupons());
        }

        [HttpGet("{id}")]
        //[Authorize]
        //todo: retornar com a autenticação 
        public async Task<IActionResult> get(Guid id)
        {
            return Ok(await _service.BuscarCupomPorId(id));
        }

        [HttpPost]
        //[Authorize]
        // todo: Retornar com  aautenticação 
        public async Task<IActionResult> post(Cupom request)
        {
            var Resultado = await _service.AdicionarCupom(request);
            return Ok(Resultado);
        }
    }
}
