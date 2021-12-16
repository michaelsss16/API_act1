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
        public void Get_RecebeMensagemComRetornoDeTodosOsClientes()
        {
            var lista = new List<Cliente>();
            var appService = new Mock<IClienteAppService>();
            appService.Setup(x => x.BuscarTodosOsClientes()).ReturnsAsync(lista);
            var controller = new ClientesController(appService.Object);
            var resultado = controller.Get().Result;
            IActionResult esperado = Ok(lista);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void Get_cpf_RetornaClienteComOsDadosReferentesAoCPFInformado()
        {
            var cpf = "11111111111";
            var cliente = new Cliente() { CPF = cpf };
            var appService = new Mock<IClienteAppService>();
            appService.Setup(x => x.BuscarClientePorCPF(cpf)).ReturnsAsync(cliente);
            var controller = new ClientesController(appService.Object);
            var resultado = controller.get(cpf).Result;
            IActionResult esperado = Ok(cliente);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void post_recebeMensagemPositivaParaAdicaoDeCliente()
        {
            var cliente = new Cliente();
            var appService = new Mock<IClienteAppService>();
            appService.Setup(x => x.ValidarEAdicionarCliente(cliente)).ReturnsAsync("Cliente adicionado com sucesso");
            var controller = new ClientesController(appService.Object);
            var resultado = controller.post(cliente).Result;
            IActionResult esperado = Ok("Cliente adicionado com sucesso");
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }



    } // Fim da classe 
}
