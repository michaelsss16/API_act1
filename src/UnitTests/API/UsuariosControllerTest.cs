using API.Controllers;
using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.API
{
    public class UsuariosControllerTest : ControllerBase
    {
        [Fact]
        public async void Get_RecebeMensagemComRetornoDeTodosOsUsuarios()
        {
            // Arrange
            var lista = new List<UsuarioGetDTO>();
            var appService = new Mock<IUsuarioAppService>();
            appService.Setup(x => x.BuscarrTodosOsUsuarios()).ReturnsAsync(lista);
            var controller = new UsuariosController(appService.Object);

            // Act
            var resultado = await controller.Get() as OkObjectResult;

            // Assert
            var esperado = Ok(lista);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void Get_Id_RetornaUsuarioConformeId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var usuario = new UsuarioGetDTO() { Id= id};
            var appService = new Mock<IUsuarioAppService>();
            appService.Setup(x => x.BuscarUsuarioPorId(id)).ReturnsAsync(usuario);
            var controller = new UsuariosController(appService.Object);

            // Act
            var resultado = await controller.Get(id) as OkObjectResult;

            // Assert
            var esperado = Ok(usuario);
            Assert.Equal(esperado.Value, resultado.Value);
        }

        [Fact]
        public async void post_recebeMensagemPositivaParaAdicaoDeUsuario()
        {
            // Assert
            var usuario = new UsuarioDTO();
            var appService = new Mock<IUsuarioAppService>();
            appService.Setup(x => x.AdicionarUsuario(usuario)).ReturnsAsync("Usuário adicionado com sucesso");
            var controller = new UsuariosController(appService.Object);

            // Act
            var resultado = await controller.Post(usuario) as OkObjectResult;

            // Assert
            var esperado = Ok("Usuário adicionado com sucesso");
            Assert.Equal(esperado.Value, resultado.Value);
        }
    }
}
