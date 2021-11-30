using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Repositories;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _Repository;
        private readonly IClienteService _Service;

        public ClientesController(IClienteRepository repository, IClienteService service)
        {
            _Repository = repository;
            _Service = service;
        } 

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _Repository.BuscarTodosOsClientes());
        }

        [HttpPost]
        public async Task<IActionResult> post(Cliente request)
        {
            if (!(await _Service.ValidarCPF(request))) {
                return Ok("O CPF informado não é válido.");
            }
            if (await _Service.ValidarCadastro(request)) {
                return Ok("Já existe entrada com esse valor");
            }
            var Resultado = await Task.Run(() => _Repository.AdicionarCliente(request));
            return Ok(Resultado);
        }
    }
}
