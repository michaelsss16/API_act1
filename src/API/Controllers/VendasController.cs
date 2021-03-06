using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Domain.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

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
        //[Authorize]
        // todo: retornar com a autenticação 
        public async Task<IActionResult> Get()
        {
            return Ok(await _Service.BuscarTodasAsVendas());
        }

        [HttpGet("{id}")]
        //[Authorize]
        // todo: retornar com a autenticação 
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _Service.BuscarVendaPorId(id));
        }

        [Route("cpf/{Cpf}")]
        [HttpGet]
        //[Authorize(Roles = "cliente")]
        // todo:retornar autenticação 
        public async Task<IActionResult> GetCpf(string Cpf)
        {
            var Result = await _Service.BuscarVendasPorCPF(User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            return Ok(Result);
        }

        [HttpPost]
        //[Authorize(Roles = "cliente")]
        // todo: retornar autenticação 
        public async Task<IActionResult> Post(VendaDTO vendadto)
        {
            // todo: Adicionar tratamento de exceção quando o jwt já está expirado 
            vendadto.CPF = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            //todo: Fazer verificação entre o cpf informado pelo jwt e objeto 
            return Ok(await _Service.AdicionarVenda(vendadto));
        }

    }
}
