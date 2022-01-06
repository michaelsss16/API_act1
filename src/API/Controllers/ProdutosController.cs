using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Domain.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoAppService _Service;
        public ProdutosController(IProdutoAppService service)
        {
            _Service = service;
        }

        [HttpGet]
        //[Authorize]
        //todo: retornar autenticação 
        public async Task<IActionResult> get()
        {
            return Ok(await _Service.BuscarTodosOsProdutos());
        }

        [HttpGet("{ID}")]
        //[Authorize]
        // todo: retornar com a autenticação 
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _Service.BuscarProdutoPorId(id));
        }

        [HttpPost]
        //[Authorize(Roles = "administrador")]
        // todo: retornar com a autenticação 
        public async Task<IActionResult> Post(ProdutoDTO produto)
        {
            return Ok(await _Service.AdicionarProduto(produto));
        }
    }
}
