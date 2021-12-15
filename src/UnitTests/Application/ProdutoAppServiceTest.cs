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
        public void BusarTodosOsProdutos_RetornaListaDeProdutos()
        {
            var lista = new List<Produto>() as IEnumerable<Produto>;
            var service = new Mock<IProdutoService>();
            service.Setup(p => p.BuscarTodosOsProdutos()).ReturnsAsync(lista);
            var appService = new ProdutoAppService(service.Object);
            var resultado = appService.BuscarTodosOsProdutos().Result;
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void BuscarProdutoPorId_RetornaProdutoDeAcordoComOId()
        {
            var id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id };
            var service = new Mock<IProdutoService>();
            service.Setup(p => p.BuscarProdutoPorId(id)).ReturnsAsync(produto);
            var appService = new ProdutoAppService(service.Object);
            var resultado = appService.BuscarProdutoPorId(id).Result;
            Assert.NotNull(resultado);
            Assert.Equal(produto, resultado);
        }

[Fact]
    public void AdicionarProduto_RetornaMensagemPositivaParaAdicao()
    {
        var produtodto = new ProdutoDTO();
        var service = new Mock<IProdutoService>();
        service.Setup(p => p.AdicionarProduto(produtodto)).ReturnsAsync("Produto adicionado com sucesso");
        var appService = new ProdutoAppService(service.Object);
        var resultado = appService.AdicionarProduto(produtodto).Result;
        Assert.NotNull(resultado);
        Assert.Equal("Produto adicionado com sucesso", resultado);
    }
}// fim da classe
}
