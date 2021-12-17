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
    public class VendasControllerTest : ControllerBase
    {

        [Fact]
        public async void Get_RecebeAListaDeTodasAsVendas()
        {
            // Arrange
            var lista = new List<Venda>();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.BuscarTodasAsVendas()).ReturnsAsync(lista);
            var controller = new VendasController(appService.Object);

            // Act
            var resultado = await controller.Get() as OkObjectResult;

            // Assert
            var esperado = Ok(lista);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void get_id_RetornaAVendaCorrespondenteAoIdInformado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var venda = new Venda();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.BuscarVendaPorId(id)).ReturnsAsync(venda);
            var controller = new VendasController(appService.Object);

            // Act
            var resultado = await controller.Get(id) as OkObjectResult;

            // Assert
            var esperado = Ok(venda);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void Get_CPF_RetornaVendaCorrespondenteAoCPF()
        {
            // Arrange 
            var cpf = "11111111111";
            var lista = new List<Venda>();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.BuscarVendasPorCPF(cpf)).ReturnsAsync(lista);
            var controller = new VendasController(appService.Object);

            // Act
            var resultado = await controller.GetCpf(cpf) as OkObjectResult;

            // Assert
            var esperado = Ok(lista);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void Post_RetornaMensagemDeSucessoParaAdicaoDeNovaVendaDTO()
        {
            // Arrange
            var vendadto = new VendaDTO();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.AdicionarVenda(vendadto)).ReturnsAsync("Venda adicionada com sucesso");
            var controller = new VendasController(appService.Object);

            // Act
            var resultado = await controller.Post(vendadto) as OkObjectResult;

            // Assert
            var esperado = Ok("Venda adicionada com sucesso");
            Assert.Equal(esperado.Value, resultado.Value);
        }
    }
}
