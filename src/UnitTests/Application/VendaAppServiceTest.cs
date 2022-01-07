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
        public async void BuscarTodasAsVendas_RetornaAListaDeTodasAsVendas()
        {
            // Arrange
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            var servicecupom = new Mock<ICupomService>();
            servicev.Setup(p => p.BuscarTodasAsVendas()).ReturnsAsync(lista);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object, servicecupom.Object);

            // Act
            var resultado = await appService.BuscarTodasAsVendas();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async void BuscarVendaPorId_RetornaAVendaCorrespondenteAoIdInserido()
        {
            // Arrange
            var id = Guid.NewGuid();
            var venda = new Venda() { Id = id };
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            var servicecupom= new Mock<ICupomService>();
            servicev.Setup(p => p.BuscarVendaPorId(id)).ReturnsAsync(venda);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object, servicecupom.Object);

            // Act
            var resultado = await appService.BuscarVendaPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(venda, resultado);
        }

        [Fact]
        public async void BuscarVendaPorCPF_RetornaListaDeVendasPeloCPF()
        {
            // Arrange
            var cpf = "11111111111";
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicecupom = new Mock<ICupomService>();
            var servicec = new Mock<IClienteService>();
            servicev.Setup(p => p.BuscarVendasPorCPF(cpf)).ReturnsAsync(lista);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object, servicecupom.Object);

            // Act
            var resultado = await appService.BuscarTodasAsVendas();
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async void AdicionarVenda_RetornoDeMensagemPositivaParaChamadaCorretaDeTodosOsServicos()
        {
            // Arrange 
            var vendadto = new VendaDTO() { ListaProdutos = new List<ProdutoVendaDTO>() };
            var valor = 0;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            var servicecupom = new Mock<ICupomService>();
            servicev.Setup(p => p.AdicionarVenda(vendadto, valor, 0.0)).ReturnsAsync("Venda adicionada com sucesso" );
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object,  servicecupom.Object);

            // Act
            var resultado = await appService.AdicionarVenda(vendadto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Venda adicionada com sucesso", resultado);
        }

        [Fact]
        public async void AdicionarVenda_RetornoDeMensagemDeErroParaProblemaDeValidacao()
        {
            // Arrange 
            var vendadto = new VendaDTO() { ListaProdutos = new List<ProdutoVendaDTO>(), CPF = "11111111111" };
            var valor = 0;
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            var servicecupom = new Mock<ICupomService>();
            servicev.Setup(p => p.AdicionarVenda(vendadto, valor, 0)).ReturnsAsync("Venda adicionada com sucesso");
            servicec.Setup(p => p.BuscarClientePorCPF("11111111111")).Throws(new Exception("Cliente não encontrado"));
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object,servicecupom.Object);

            // Act
            var resultado = await appService.AdicionarVenda(vendadto);

            // Assert
            Assert.Equal("Cliente não encontrado", resultado);
        }

        [Fact]
        public async void CalcularValorDaVenda_RetornaValorCorrespondenteAVendaDTOPassada()
        {
            // Arrange
            var vendadto = new VendaDTO() { ListaProdutos = new List<ProdutoVendaDTO>() };
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            var servicecupom = new Mock<ICupomService>();
            servicev.Setup(p => p.CalcularValorDaVenda(It.IsAny<VendaDTO>(), It.IsAny<List<Produto>>())).Returns(100);
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object, servicecupom.Object);

            // Act
            var resultado = await appService.CalcularValorDaVenda(vendadto);

            // Assert
            Assert.Equal(100, resultado);
        }

        [Fact]
        public async void AtualizarQuantidadeDeProdutos_RetornaMensagemDeConfirmacaoParaAChamada()
        {
            // Arrange
            var lista = new List<Venda>() as IEnumerable<Venda>;
            var vendadto = new VendaDTO() { ListaProdutos = new List<ProdutoVendaDTO>() };
            var servicev = new Mock<IVendaService>();
            var servicep = new Mock<IProdutoService>();
            var servicec = new Mock<IClienteService>();
            var servicecupom = new Mock<ICupomService>();
            servicep.Setup(p => p.AtualizarListaDeProdutos(vendadto.ListaProdutos)).ReturnsAsync("Quantidades atualizadas com sucesso");
            var appService = new VendaAppService(servicev.Object, servicep.Object, servicec.Object, servicecupom.Object);

            // Act
            var resultado = await appService.AtualizarQuantidadeDeProdutos(vendadto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Quantidades atualizadas com sucesso", resultado);
        }
    }
}
