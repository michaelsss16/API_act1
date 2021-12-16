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
        public void Get_RecebeAListaDeTodasAsVendas()
        {
            var lista = new List<Venda>();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.BuscarTodasAsVendas()).ReturnsAsync(lista);
            var controller = new VendasController(appService.Object);
            var resultado = controller.Get().Result;
            IActionResult esperado = Ok(lista);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void get_id_RetornaAVendaCorrespondenteAoIdInformado()
        {
            var id = Guid.NewGuid();
            var venda = new Venda();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.BuscarVendaPorId(id)).ReturnsAsync(venda);
            var controller = new VendasController(appService.Object);
            var resultado = controller.Get(id).Result;
            IActionResult esperado = Ok(venda);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void Get_CPF_RetornaVendaCorrespondenteAoCPF()
        {
            var cpf = "11111111111";
            var lista = new List<Venda>();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.BuscarVendasPorCPF(cpf)).ReturnsAsync(lista);
            var controller = new VendasController(appService.Object);
            var resultado = controller.GetCpf(cpf).Result;
            IActionResult esperado = Ok(lista);
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

        [Fact]
        public void Post_RetornaMensagemDeSucessoParaAdicaoDeNovaVendaDTO()
        {
            var vendadto = new VendaDTO();
            var appService = new Mock<IVendaAppService>();
            appService.Setup(x => x.AdicionarVenda(vendadto)).ReturnsAsync("Venda adicionada com sucesso");
            var controller = new VendasController(appService.Object);
            var resultado = controller.Post(vendadto).Result;
            IActionResult esperado = Ok("Venda adicionada com sucesso");
            Assert.Equal(esperado.ToString(), resultado.ToString());
        }

    }// Fim da classe 
}
