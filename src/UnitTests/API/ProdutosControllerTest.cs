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
        public async void Get_RecebeAListaDeTodosOsProdutos()
        {
            // Arrange
            var lista = new List<Produto>();
            var appService = new Mock<IProdutoAppService>();
            appService.Setup(x => x.BuscarTodosOsProdutos()).ReturnsAsync(lista);
            var controller = new ProdutosController(appService.Object);

            // Act
            var resultado = await controller.get() as OkObjectResult;

            // Assert
            var esperado = Ok(lista);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void get_id_RetornaOProdutoCorrespondenteAoIdInformado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var produto = new Produto() { Id = id };
            var appService = new Mock<IProdutoAppService>();
            appService.Setup(x => x.BuscarProdutoPorId(id)).ReturnsAsync(produto);
            var controller = new ProdutosController(appService.Object);

            // Act
            var resultado = await controller.Get(id) as OkObjectResult;

            // Assert
            var esperado = Ok(produto);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void post_RetornaMensagemPositivaParaPassagemDoProdutoASerAdicionado()
        {
            // Arrange
            var produtodto = new ProdutoDTO();
            var appService = new Mock<IProdutoAppService>();
            appService.Setup(x => x.AdicionarProduto(produtodto)).ReturnsAsync("Produto adicionado com sucesso");
            var controller = new ProdutosController(appService.Object);

            // Act
            var resultado = await controller.Post(produtodto) as OkObjectResult;

            // Assert
            var esperado = Ok("Produto adicionado com sucesso");
            Assert.Equal(esperado.Value, resultado.Value);
        }
    }
}
