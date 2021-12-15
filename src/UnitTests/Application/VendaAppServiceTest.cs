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
    public class VendaAppServiceTest
    {
        [Fact]
        public void BuscarTodasAsVendas_RetornaAListaDeTodasAsVendas()
        {
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var servicev= new Mock<IVendaService>();
            var servicep= new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.BuscarTodasAsVendas()).ReturnsAsync(lista);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.BuscarTodasAsVendas().Result;
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void BuscarVendaPorId_RetornaAVendaCorrespondenteAoIdInserido()
        {
            var id = Guid.NewGuid();
            var venda = new Venda() { Id = id};
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.BuscarVendaPorId(id)).ReturnsAsync(venda);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.BuscarVendaPorId(id).Result;
            Assert.NotNull(resultado);
            Assert.Equal(venda, resultado);
        }

        [Fact]
        public void BuscarVendaPorCPF_RetornaListaDeVendasPeloCPF()
        {
            var cpf = "11111111111";
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.BuscarVendasPorCPF(cpf)).ReturnsAsync(lista);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.BuscarTodasAsVendas().Result;
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }
        [Fact]
        public void AdicionarVenda_RetornoDeMensagemPositivaParaChamadaCorretaDeTodosOsServicos()
        {
            var vendadto = new VendaDTO() {ListaProdutos = new List<ProdutoVendaDTO>()};
            var valor = 0;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.AdicionarVenda(vendadto, valor)).ReturnsAsync("Venda adicionada com sucesso");
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.AdicionarVenda(vendadto).Result;
            Assert.NotNull(resultado);
            Assert.Equal("Venda adicionada com sucesso", resultado);
        }

        [Fact]
        public void AdicionarVenda_RetornoDeMensagemDeErroParaProblemaDeValidacao()
        {
            var vendadto = new VendaDTO() { ListaProdutos = new List<ProdutoVendaDTO>(), CPF = "11111111111" };
            var valor = 0;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.AdicionarVenda(vendadto, valor)).ReturnsAsync("Venda adicionada com sucesso");
            servicec.Setup(p => p.BuscarClientePorCPF("11111111111")).Throws(new Exception("Cliente não encontrado"));
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.AdicionarVenda(vendadto).Result;
            Assert.Equal("Cliente não encontrado", resultado);
        }

        [Fact]
        public void CalcularValorDaVenda_RetornaValorCorrespondenteAVendaDTOPassada()
        {
            var vendadto = new VendaDTO() {ListaProdutos = new List<ProdutoVendaDTO>()  };
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.CalcularValorDaVenda(It.IsAny<VendaDTO>(),It.IsAny<List<Produto>>())).Returns(100);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.CalcularValorDaVenda(vendadto).Result;
            Assert.NotNull(resultado);
            Assert.Equal(100, resultado);
        }

        [Fact]
        public void AtualizarQuantidadeDeProdutos_RetornaMensagemDeConfirmacaoParaAChamada()
        {
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var vendadto = new VendaDTO() {ListaProdutos = new List<ProdutoVendaDTO>() };
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            servicep.Setup(p => p.AtualizarListaDeProdutos(vendadto.ListaProdutos)).ReturnsAsync("Quantidades atualizadas com sucesso");
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object);
            var resultado = appService.AtualizarQuantidadeDeProdutos(vendadto).Result;
            Assert.NotNull(resultado);
            Assert.Equal("Quantidades atualizadas com sucesso", resultado);
        }


    }// Fim da classe
}
