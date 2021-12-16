using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Domain.Entities;
using Domain.DTO;
using API.Controllers;
using Application.Interfaces;

namespace UnitTests.API
{
    public class ProdutosControllerTest : ControllerBase
    {
        [Fact]
        public void Get_RecebeAListaDeTodosOsProdutos()
        {
            var lista = new List<Produto>();
            var appService = new Mock<IProdutoAppService>();
            appService.Setup(x => x.BuscarTodosOsProdutos()).ReturnsAsync(lista);
            var controller = new ProdutosController(appService.Object);
            var resultado = controller.get().Result;
            IActionResult esperado = Ok(lista);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void get_id_RetornaOProdutoCorrespondenteAoIdInformado()
        {
            var id = Guid.NewGuid();
            var produto = new Produto() { Id = id };
            var appService = new Mock<IProdutoAppService>();
            appService.Setup(x => x.BuscarProdutoPorId(id)).ReturnsAsync(produto);
            var controller = new ProdutosController(appService.Object);
            var resultado = controller.Get(id).Result;
            IActionResult esperado = Ok(produto);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void post_RetornaMensagemPositivaParaPassagemDoProdutoASerAdicionado()
        {
            var produtodto = new ProdutoDTO();
            var appService = new Mock<IProdutoAppService>();
            appService.Setup(x => x.AdicionarProduto(produtodto)).ReturnsAsync("Produto adicionado com sucesso");
            var controller = new ProdutosController(appService.Object);
            var resultado = controller.Post(produtodto).Result;
            IActionResult esperado = Ok(produtodto);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

    }// Fim da classe 
}
