using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces;
using Application.AppServices;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteAppService _Service;

        public ClientesController(IClienteAppService service)
        {
            _Service = service;
        }

        [HttpGet]
        //[Authorize]
        //Todo: Retornar a autenticação 
        public async Task<IActionResult> Get()
        {
            return Ok(await _Service.BuscarTodosOsClientes());
        }

        [HttpGet("{cpf}")]
        //[Authorize]
        //todo: retornar com a autenticação 
        public async Task<IActionResult> get(string cpf)
        {
            return Ok(await _Service.BuscarClientePorCPF(cpf));
        }

        [HttpPost]
        //[Authorize]
        // todo: Retornar com  aautenticação 
        public async Task<IActionResult> post(Cliente request)
        {
            var Resultado = await _Service.ValidarEAdicionarCliente(request);
            return Ok(Resultado);
        }
    }
}
