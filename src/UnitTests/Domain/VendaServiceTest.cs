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
        public void BuscarTodasAsVendas_DeveRetornarListaDeVendas()
        {
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new VendaService(repository.Object);
            var resultado = service.BuscarTodasAsVendas().Result;
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void BuscarVendaPorId_RetornaObjetoVendaParaIdCorreto()
        {
            var id = Guid.NewGuid();
            var venda = new Venda() { Id = id };
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get(id)).ReturnsAsync(venda);
            var service = new VendaService(repository.Object);
            var resultado = service.BuscarVendaPorId(id).Result;
            Assert.Equal(venda, resultado);
        }

        [Fact]
        public void AdicionarVenda_RetornaMensagemParaValoresCorretos()
        {
            var valor = 100;
            var vendadto = new VendaDTO();
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Add(It.IsAny<Venda>())).ReturnsAsync("Venda adicionada com sucesso");
            var service = new VendaService(repository.Object);
            var resultado = service.AdicionarVenda(vendadto, valor).Result;
            Assert.Equal("Venda adicionada com sucesso", resultado);
        }
        [Fact]
        public async Task BuscaVendaPorCPF_LnacaExcecaoParaValorDeCPFInvalido()
        {
            string cpf = "11111111111";
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new VendaService(repository.Object);
            await Assert.ThrowsAsync<Exception>(() => service.BuscarVendasPorCPF(cpf));
        }

        [Fact]
        public void BuscaVendaPorCPF_RetornaVendaParaCPFValido()
        {
            string cpf = "11111111111";
            var venda = new Venda() { CPF = cpf };
            var lista = new List<Venda>() { venda } as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new VendaService(repository.Object);
            var resultado = service.BuscarVendasPorCPF(cpf).Result;
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void BuscaVendaPorCPF_RetornaVendaParaCPFValidoComMaisDeumRetornoDeVenda()
        {
            string cpf = "11111111111";
            var venda = new Venda() { CPF = cpf };
            var venda2 = new Venda() { CPF = "11111111112" };
            var lista = new List<Venda>() { venda } as IEnumerable<Venda>;
            var lista2 = new List<Venda>() { venda, venda2 } as IEnumerable<Venda>;
            var repository = new Mock<IVendaRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista2);
            var service = new VendaService(repository.Object);
            var resultado = service.BuscarVendasPorCPF(cpf).Result;
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]

    } // Fim da classe
}
