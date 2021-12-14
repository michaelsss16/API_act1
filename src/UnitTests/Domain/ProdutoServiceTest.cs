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

        [Fact]
        public void BuscarProdutoPorId_TesteComRetornoPreenchidoEComCorrespondencia()
        {
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Nome = "ProdutoTeste", Quantidade = 2, Valor = 12345, Descricao = "Produto de teste" };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);
            var Resultado = service.BuscarProdutoPorId(id).Result;
            Assert.Equal(Resultado, produto);
            Assert.Equal(Resultado.Id, id);
        }

        [Fact]
        public void BuscarListaDeProdutos_RetornaAListaDosProdutosCorrespondentesAListaPassada()
        {
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
            var Resultado = service.BuscarListaDeProdutosPorId(lista).Result;
            Assert.Equal(Resultado, listaProdutos);
        }

        [Fact]
        public void BuscarListaDeProdutos_PassagemDeListaVazia()
        {
            List<Guid> lista = new List<Guid>();
            var repository = new Mock<IProdutoRepository>();
            ProdutoService service = new ProdutoService(repository.Object);
            var Resultado = service.BuscarListaDeProdutosPorId(lista).Result;
            Assert.Equal(Resultado, new List<Produto>());
        }

        [Fact]
        public void AdicionarProduto_DeveRetornarAMensagemDeRetornoCorreta()
        {
            ProdutoDTO produtodto = new ProdutoDTO();
            Produto produto = new Produto();
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Add(It.IsAny<Produto>())).ReturnsAsync("Produto adicionado com sucesso");
            ProdutoService service = new ProdutoService(repository.Object);
            string Resultado = service.AdicionarProduto(produtodto).Result;
            Assert.Equal("Produto adicionado com sucesso", Resultado);
        }

        [Fact]
        public void AtualizarProduto_DeveRetornarAMensagemDeRetornoCorreta()
        {
            Produto produto = new Produto();
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Update(It.IsAny<Produto>())).ReturnsAsync("Produto atualizado com sucesso");
            ProdutoService service = new ProdutoService(repository.Object);
            string Resultado = service.AtualizarProduto(produto).Result;
            Assert.Equal("Produto atualizado com sucesso", Resultado);
        }
        [Fact]
        public void ValidarVenda_PassagemDeListaVazia()
        {
            Exception ex = null;
            var lista = new List<ProdutoVendaDTO>();
            var repository = new Mock<IProdutoRepository>();
            ProdutoService service = new ProdutoService(repository.Object);
            try { service.ValidarVenda(lista); }
            catch (Exception e) { ex = e; }
            Assert.Null(ex);
        }

        [Fact]
        public void ValidarVenda_PassagemDeListaPreenchida()
        {
            Exception ex = null;
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Quantidade = 3, };
            ProdutoVendaDTO venda = new ProdutoVendaDTO() { Id = id, Quantidade = 2 };
            var lista = new List<ProdutoVendaDTO>() { venda };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);
            try { service.ValidarVenda(lista); }
            catch (Exception e) { ex = e; }
            Assert.Null(ex);
        }

        [Fact]
        public async void ValidarVenda_ListaComGuidErradoDeveLancarException()
        {
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Quantidade = 3 };
            ProdutoVendaDTO venda = new ProdutoVendaDTO() { Id = Guid.NewGuid(), Quantidade = 1 };
            var lista = new List<ProdutoVendaDTO>() { venda };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);
            await Assert.ThrowsAsync<Exception>(() => service.ValidarVenda(lista));
        }

        [Fact]
        public async void ValidarVenda_PassagemDeListaPreenchidaComQuantidadeExcedente()
        {
            Guid id = Guid.NewGuid();
            Produto produto = new Produto() { Id = id, Quantidade = 3 };
            ProdutoVendaDTO venda = new ProdutoVendaDTO() { Id = id, Quantidade = 4 };
            var lista = new List<ProdutoVendaDTO>() { venda };
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(produto);
            ProdutoService service = new ProdutoService(repository.Object);
            await Assert.ThrowsAsync<Exception>(() => service.ValidarVenda(lista));
        }

        [Fact]
        public void AtualizarListaDeProdutos_DeveRetornarMensagemParaListaVazia()
        {
            var lista = new List<ProdutoVendaDTO>();
            var repository = new Mock<IProdutoRepository>();
            var service = new ProdutoService(repository.Object);
            string resultado = service.AtualizarListaDeProdutos(lista).Result;
            Assert.Equal("Produtos atualizados com sucesso", resultado );
        }

        [Fact]
        public void AtualizarListaDeProdutos_DeveRetornarMensagemParaListaComProdutos()
        {
            var id1 = Guid.NewGuid();
            var produto = new Produto() { Id = id1, Quantidade = 2 };
            var lista = new List<ProdutoVendaDTO>();
            lista.Add(new ProdutoVendaDTO() { Quantidade = 1, Id = id1 });
            var repository = new Mock<IProdutoRepository>();
            repository.Setup(p => p.Get(id1)).ReturnsAsync(produto);
            repository.Setup(p => p.Update(It.IsAny<Produto>())).ReturnsAsync("Produto atualizado com sucesso");
            var service = new ProdutoService(repository.Object);
            string resultado = service.AtualizarListaDeProdutos(lista).Result;
            Assert.Equal("Produtos atualizados com sucesso", resultado );
        }


    } // Fim da classe
}
