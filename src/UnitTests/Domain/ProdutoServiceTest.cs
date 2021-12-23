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
using Domain.DTO;

namespace UnitTests.Domain
{
    public class ProdutoServiceTest
    {
        [Fact]
        public async void BuscarTodosOsProdutos_TesteDeRetornoNulo()
        {
            // Arrange 
            var repository = new Mock<IProdutoRepository>();
            ProdutoService service = new ProdutoService(repository.Object);

            // Act 
            var Resultado = await service.BuscarTodosOsProdutos();

            // Assert
            Assert.Equal(Resultado, new List<Produto>());
        }

        [Fact]
        public async void BuscarTodosOsProdutos_TesteDeRetornoParaProdutoComCamposNulos()
        {
            // Arrange 
            Produto produtoNulo = new Produto();
            IEnumerable<Produto> Lista = new List<Produto>() { produtoNulo } as IEnumerable<Produto>;
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(Lista);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            var Resultado = await service.BuscarTodosOsProdutos();

            // Assert 
            Assert.Equal(Resultado, Lista);
        }

        [Fact]
        public async void BuscarTodosOsProdutos_TesteDeRetornoParaProdutoComCamposPreenchidos()
        {
            // Arrange 
            Produto produto = new Produto() { Id = Guid.NewGuid(), Nome = "ProdutoTeste", Quantidade = 2, Valor = 12345, Descricao = "Produto de teste" };
            IEnumerable<Produto> Lista = new List<Produto>() { produto } as IEnumerable<Produto>;
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(Lista);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            var Resultado = await service.BuscarTodosOsProdutos();

            // Assert
            Assert.Equal(Resultado, Lista);
        }

        [Fact]
        public async void BuscarProdutoPorId_TesteComRetornoPreenchidoEComCorrespondencia()
        {
            // Arrange 
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Nome = "ProdutoTeste", Quantidade = 2, Valor = 12345, Descricao = "Produto de teste" };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            var Resultado = await service.BuscarProdutoPorId(id);

            // Assert 
            Assert.Equal(Resultado, produto);
            Assert.Equal(Resultado.Id, id);
        }

        [Fact]
        public async void BuscarListaDeProdutos_RetornaAListaDosProdutosCorrespondentesAListaPassada()
        {
            // Arrange 
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            Produto p1 = new Produto() { Id = id1 };
            Produto p2 = new Produto() { Id = id2 };
            List<Guid> lista = new List<Guid>() { id1, id2 };
            IEnumerable<Produto> listaProdutos = new List<Produto>() { p1, p2 } as IEnumerable<Produto>;
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id1)).ReturnsAsync(p1);
            repository.Setup(p => p.Get(id2)).ReturnsAsync(p2);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            var Resultado = await service.BuscarListaDeProdutosPorId(lista);

            // Assert
            Assert.Equal(Resultado, listaProdutos);
        }

        [Fact]
        public async void BuscarListaDeProdutos_PassagemDeListaVazia()
        {
            // Arrange 
            List<Guid> lista = new List<Guid>();
            var repository = new Mock<IProdutoRepository>();
            ProdutoService service = new ProdutoService(repository.Object);

            // Act 
            var Resultado = await service.BuscarListaDeProdutosPorId(lista);

            // Assert 
            Assert.Equal(Resultado, new List<Produto>());
        }

        [Fact]
        public async void AdicionarProduto_DeveRetornarAMensagemDeRetornoCorreta()
        {
            // Arrange 
            ProdutoDTO produtodto = new ProdutoDTO();
            Produto produto = new Produto();
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Add(It.IsAny<Produto>())).ReturnsAsync("Produto adicionado com sucesso");
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            string Resultado = await service.AdicionarProduto(produtodto);

            // Assert
            Assert.Equal("Produto adicionado com sucesso", Resultado);
        }

        [Fact]
        public async void AtualizarProduto_DeveRetornarAMensagemDeRetornoCorreta()
        {
            // Arrange 
            Produto produto = new Produto();
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Update(It.IsAny<Produto>())).ReturnsAsync("Produto atualizado com sucesso");
            ProdutoService service = new ProdutoService(repository.Object);

            // Act 
            string Resultado = await service.AtualizarProduto(produto);

            // Assert
            Assert.Equal("Produto atualizado com sucesso", Resultado);
        }

        [Fact]
        public async void ValidarVenda_PassagemDeListaVazia()
        {
            // Arrange 
            Exception ex = null;
            var lista = new List<ProdutoVendaDTO>();
            var repository = new Mock<IProdutoRepository>();
            ProdutoService service = new ProdutoService(repository.Object);

            // /Act
            try { await service.ValidarVenda(lista); }
            catch (Exception e) { ex = e; }

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public async void ValidarVenda_PassagemDeListaPreenchida()
        {
            // Arrange 
            Exception ex = null;
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Quantidade = 3, };
            ProdutoVendaDTO venda = new ProdutoVendaDTO() { ProdutoId= id, Quantidade = 2 };
            var lista = new List<ProdutoVendaDTO>() { venda };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            try { await service.ValidarVenda(lista); }
            catch (Exception e) { ex = e; }

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public async void ValidarVenda_ListaComGuidErradoDeveLancarException()
        {
            // Arrange 
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Quantidade = 3 };
            ProdutoVendaDTO venda = new ProdutoVendaDTO() { ProdutoId= Guid.NewGuid(), Quantidade = 1 };
            var lista = new List<ProdutoVendaDTO>() { venda };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => service.ValidarVenda(lista));
        }

        [Fact]
        public async void ValidarVenda_PassagemDeListaPreenchidaComQuantidadeExcedente()
        {
            // Arrange 
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id= id, Quantidade = 3 };
            ProdutoVendaDTO venda = new ProdutoVendaDTO() { ProdutoId= id, Quantidade = 4 };
            var lista = new List<ProdutoVendaDTO>() { venda };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => service.ValidarVenda(lista));
        }

        [Fact]
        public async void AtualizarListaDeProdutos_DeveRetornarMensagemParaListaVazia()
        {
            // Arrange
            var lista = new List<ProdutoVendaDTO>();
            var repository = new Mock<IProdutoRepository>();
            var service = new ProdutoService(repository.Object);

            // Act
            string resultado = await service.AtualizarListaDeProdutos(lista);

            // Assert
            Assert.Equal("Produtos atualizados com sucesso", resultado);
        }

        [Fact]
        public async void AtualizarListaDeProdutos_DeveRetornarMensagemParaListaComProdutos()
        {
            // Arrange 
            var id1 = Guid.NewGuid();
            var produto = new Produto() { Id = id1, Quantidade = 2 };
            var lista = new List<ProdutoVendaDTO>();
            lista.Add(new ProdutoVendaDTO() { Quantidade = 1, ProdutoId= id1 });
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id1)).ReturnsAsync(produto);
            repository.Setup(p => p.Update(It.IsAny<Produto>())).ReturnsAsync("Produto atualizado com sucesso");
            var service = new ProdutoService(repository.Object);

            // Act
            string resultado = await service.AtualizarListaDeProdutos(lista);

            // Assert
            Assert.Equal("Produtos atualizados com sucesso", resultado);
        }
    }
}
