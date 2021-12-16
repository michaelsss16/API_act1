using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Domain.DTO;
using Application.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IVendaAppService _Service;

        public VendasController(IVendaAppService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _Service.BuscarTodasAsVendas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _Service.BuscarVendaPorId(id));
        }

        [Route("cpf/{Cpf}")]
        [HttpGet]
        public async Task<IActionResult> GetCpf(string Cpf)
        {
            var Result = await _Service.BuscarVendasPorCPF(Cpf);
            return Ok(Result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendaDTO vendadto)
        {
            return Ok(await _Service.AdicionarVenda(vendadto));
        }

    }
}
