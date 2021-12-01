using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Entities;
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
        public async Task<string> get()
        {
            return "Este é o primeiro teste de retorno";
        }
    }
}
