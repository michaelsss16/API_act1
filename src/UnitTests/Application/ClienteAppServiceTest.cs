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
    public class ClienteAppServiceTest
    {
        [Fact]
        public void ValidarEAdicionarCliente_RetornaMensagemPositivaParaValoresCorretosDeEntrada()
        {
            var cliente = new Cliente();
            var service = new Mock<IClienteService>();
            service.Setup(p => p.CadastrarCliente(cliente)).ReturnsAsync("Cliente cadastrado com sucesso");
            var appService = new ClienteAppService(service.Object);
            var resultado = appService.ValidarEAdicionarCliente(cliente).Result;
            Assert.NotNull(resultado);
            Assert.Equal("Cliente cadastrado com sucesso", resultado);
        }

        [Fact]
        public async void ValidarEAdicionarCliente_LancaExcecaoParaErroDeValidacao()
        {
            var cliente = new Cliente();
            var service = new Mock<IClienteService>();
            service.Setup(p => p.CadastrarCliente(cliente)).ReturnsAsync("Cliente cadastrado com sucesso");
            service.Setup(p => p.ValidarTodasAsRegras(It.IsAny<Cliente>())).ThrowsAsync(new Exception("erro"));
            var appService = new ClienteAppService(service.Object);
            var resultado = appService.ValidarEAdicionarCliente(cliente).Result;
            Assert.Equal("erro", resultado);
        }

        [Fact]
        public void BuscarTodosOsClientes_RetornaListaDeTodosOsClientes()
        {
            var lista = new List<Cliente>() as IEnumerable<Cliente>;
            var service = new Mock<IClienteService>();
            service.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(lista);
            var appService = new ClienteAppService(service.Object);
            var resultado = appService.BuscarTodosOsClientes().Result;
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void BuscarClientePorCPF_RetornaClienteParaCPFValido()
        {
            string cpf = "11111111111";
            var cliente = new Cliente() { CPF = cpf };
            var service = new Mock<IClienteService>();
            service.Setup(p => p.BuscarClientePorCPF(cpf)).ReturnsAsync(cliente);
            var appService = new ClienteAppService(service.Object);
            var resultado = appService.BuscarClientePorCPF(cpf).Result;
            Assert.NotNull(resultado);
            Assert.Equal(cliente, resultado);
        }
    }// Fim da classe 
}
