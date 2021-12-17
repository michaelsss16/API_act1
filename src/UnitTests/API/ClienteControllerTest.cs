using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Domain.Entities;
using Domain.DTO;
using API.Controllers;
using Application.Interfaces;

namespace UnitTests.API
{
    public class ClienteControllerTest : ControllerBase
    {

        [Fact]
        public async void Get_RecebeMensagemComRetornoDeTodosOsClientes()
        {
            // Arrange
            var lista = new List<Cliente>();
            var appService = new Mock<IClienteAppService>();
            appService.Setup(x => x.BuscarTodosOsClientes()).ReturnsAsync(lista);
            var controller = new ClientesController(appService.Object);

            // Act
            var resultado = await controller.Get() as OkObjectResult;

            // Assert
            var esperado = Ok(lista);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void Get_cpf_RetornaClienteComOsDadosReferentesAoCPFInformado()
        {
            // Arrange
            var cpf = "11111111111";
            var cliente = new Cliente() { CPF = cpf };
            var appService = new Mock<IClienteAppService>();
            appService.Setup(x => x.BuscarClientePorCPF(cpf)).ReturnsAsync(cliente);
            var controller = new ClientesController(appService.Object);

            // Act
            var resultado = await controller.get(cpf) as OkObjectResult;

            // Assert
            var esperado = Ok(cliente);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void post_recebeMensagemPositivaParaAdicaoDeCliente()
        {
            // Assert
            var cliente = new Cliente();
            var appService = new Mock<IClienteAppService>();
            appService.Setup(x => x.ValidarEAdicionarCliente(cliente)).ReturnsAsync("Cliente adicionado com sucesso");
            var controller = new ClientesController(appService.Object);

            // Act
            var resultado = await controller.post(cliente) as OkObjectResult;

            // Assert
            var esperado = Ok("Cliente adicionado com sucesso");
            Assert.Equal(esperado.Value, resultado.Value);
        }
    }
}
