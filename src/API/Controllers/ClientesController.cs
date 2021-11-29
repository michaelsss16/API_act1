using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Services;
using Infrastructure.Repositories;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _Repository;

        public ClientesController(IClienteRepository repository) {
            _Repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _Repository.BuscarTodosOsClientes());
        }

        [HttpPost]
    public async Task<IActionResult> post(Cliente request) {
            var Resultado = await Task.Run(()=> _Repository.AdicionarCliente(request));
            return Ok(Resultado);
    }
    }
}
