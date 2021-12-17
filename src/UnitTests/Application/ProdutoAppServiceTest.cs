using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces.Services;
using Application.AppServices;

namespace UnitTests.Application
{
    public class ProdutoAppServiceTest
    {

        [Fact]
        public async void BusarTodosOsProdutos_RetornaListaDeProdutos()
        {
            // Arrange
            var lista = new List<Produto>() as IEnumerable<Produto>;
            var service = new Mock<IProdutoService>();
            service.Setup(p => p.BuscarTodosOsProdutos()).ReturnsAsync(lista);
            var appService = new ProdutoAppService(service.Object);

            // Act
            var resultado = await appService.BuscarTodosOsProdutos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async void BuscarProdutoPorId_RetornaProdutoDeAcordoComOId()
        {
            // Arrange 
            var id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id };
            var service = new Mock<IProdutoService>();
            service.Setup(p => p.BuscarProdutoPorId(id)).ReturnsAsync(produto);
            var appService = new ProdutoAppService(service.Object);

            // Act
            var resultado = await appService.BuscarProdutoPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(produto, resultado);
        }

        [Fact]
        public async void AdicionarProduto_RetornaMensagemPositivaParaAdicao()
        {
            // Arrange 
            var produtodto = new ProdutoDTO();
            var service = new Mock<IProdutoService>();
            service.Setup(p => p.AdicionarProduto(produtodto)).ReturnsAsync("Produto adicionado com sucesso");
            var appService = new ProdutoAppService(service.Object);

            // Acy
            var resultado = await appService.AdicionarProduto(produtodto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Produto adicionado com sucesso", resultado);
        }
    }
}
