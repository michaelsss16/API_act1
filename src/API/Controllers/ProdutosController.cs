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
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoAppService _Service;
        public ProdutosController(IProdutoAppService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _Service.BuscarTodosOsProdutos());
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(Guid id) {
            return Ok(await _Service.BuscarProdutoPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDTO produto) {
            return Ok(await _Service.AdicionarProduto(produto));
        }
    }
}
