using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Repositories;

namespace UnitTests.Domain
{
    public class ProdutoServiceTest
    {
        [Fact]
        public void BuscarTodosOsProdutos_TesteDeRetornoNulo()
        {
            var repository = new Mock<IProdutoRepository>();
            ProdutoService service = new ProdutoService(repository.Object);
            var Resultado = service.BuscarTodosOsProdutos().Result;
            Assert.Equal(Resultado, new List<Produto>());
        }
        [Fact]
        public void BuscarTodosOsProdutos_TesteDeRetornoParaProdutoComCamposNulos()
        {
            Produto produtoNulo = new Produto();
            IEnumerable<Produto> Lista = new List<Produto>() { produtoNulo } as IEnumerable<Produto>;
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(Lista);
            ProdutoService service = new ProdutoService(repository.Object);
            var Resultado = service.BuscarTodosOsProdutos().Result;
            Assert.Equal(Resultado, Lista);
        }

        [Fact]
        public void BuscarTodosOsProdutos_TesteDeRetornoParaProdutoComCamposPreenchidos()
        {
            Produto produto = new Produto() { Id = Guid.NewGuid(), Nome = "ProdutoTeste", Quantidade = 2, Valor = 12345, Descricao = "Produto de teste" };
            IEnumerable<Produto> Lista = new List<Produto>() { produto } as IEnumerable<Produto>;
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(Lista);
            ProdutoService service = new ProdutoService(repository.Object);
            var Resultado = service.BuscarTodosOsProdutos().Result;
            Assert.Equal(Resultado, Lista);
        }

    } // Fim da classe
}
