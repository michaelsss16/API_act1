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
    public class VendaServiceTest
    {

        [Fact]
        public async void BuscarTodasAsVendas_DeveRetornarListaDeVendas()
        {
            // Arrange 
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new VendaService(repository.Object);

            // Act
            var resultado = await service.BuscarTodasAsVendas();

            // Assert
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async void BuscarVendaPorId_RetornaObjetoVendaParaIdCorreto()
        {
            // Arrange 
            var id = Guid.NewGuid();
            var venda = new Venda() { Id = id };
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(venda);
            var service = new VendaService(repository.Object);

            // Act
            var resultado = await service.BuscarVendaPorId(id);

            // Assert 
            Assert.Equal(venda, resultado);
        }

        [Fact]
        public async void AdicionarVenda_RetornaMensagemParaValoresCorretos()
        {
            // Arrange 
            var valor = 100;
            var vendadto = new VendaDTO();
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Add(It.IsAny<Venda>())).ReturnsAsync("Venda adicionada com sucesso");
            var service = new VendaService(repository.Object);

            // Act
            var resultado = await service.AdicionarVenda(vendadto, valor);

            // Assert
            Assert.Equal("Venda adicionada com sucesso", resultado);
        }

        [Fact]
        public async Task BuscaVendaPorCPF_LnacaExcecaoParaValorDeCPFInvalido()
        {
            // Arrange 
            string cpf = "11111111111";
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new VendaService(repository.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => service.BuscarVendasPorCPF(cpf));
        }

        [Fact]
        public async void BuscaVendaPorCPF_RetornaVendaParaCPFValido()
        {
            // Arrange 
            string cpf = "11111111111";
            var venda = new Venda() { CPF = cpf };
            var lista = new List<Venda>() { venda } as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new VendaService(repository.Object);

            // Act
            var resultado = await service.BuscarVendasPorCPF(cpf);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async void BuscaVendaPorCPF_RetornaVendaParaCPFValidoComMaisDeumRetornoDeVenda()
        {
            // Arrange
            string cpf = "11111111111";
            var venda = new Venda() { CPF = cpf };
            var venda2 = new Venda() { CPF = "11111111112" };
            var lista = new List<Venda>() { venda } as IEnumerable<Venda>;
            var lista2 = new List<Venda>() { venda, venda2 } as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista2);
            var service = new VendaService(repository.Object);

            // Act
            var resultado = await service.BuscarVendasPorCPF(cpf);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void CalcularValorDaVenda_DeveRetornarZeroParaListaVaziaDeProdutos()
        {
            // Arrange 
            var vendadto = new VendaDTO();
            vendadto.ListaProdutos = new List<ProdutoVendaDTO>();
            var lista = new List<Produto>();
            var repository = new Mock<IVendaRepository>();
            var service = new VendaService(repository.Object);

            // Act
            var resultado = service.CalcularValorDaVenda(vendadto, lista);

            // Assert
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void CalcularValorDaVenda_DeveRetornarOValorCorretoParaOsProdutos()
        {
            // Arrange
            var vendadto = new VendaDTO();
            vendadto.ListaProdutos = new List<ProdutoVendaDTO>();
            vendadto.ListaProdutos.Add(new ProdutoVendaDTO() { Quantidade = 2 });
            vendadto.ListaProdutos.Add(new ProdutoVendaDTO() { Quantidade = 3 });
            var lista = new List<Produto>();
            lista.Add(new Produto() { Valor = 10 });
            lista.Add(new Produto() { Valor = 5 });
            var repository = new Mock<IVendaRepository>();
            var service = new VendaService(repository.Object);

            // Act
            var resultado = service.CalcularValorDaVenda(vendadto, lista);

            // Assert
            Assert.Equal(35, resultado);
        }
    }
}
