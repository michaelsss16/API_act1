using Application.AppServices;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application
{
    public class UsuarioAppServiceTest
    {
        [Fact]
        public async void AdicionarUsuario_RetornaMensagemPositivaParaEntradaCorreta()
        {
            // Arrange 
            var usuario= new UsuarioDTO();
            var service = new Mock<IUsuarioService>();
            service.Setup(p => p.AdicionarUsuario(usuario)).ReturnsAsync("Usuário cadastrado com sucesso");
            var appService = new UsuarioAppService(service.Object);

            // Act
            var resultado = await appService.AdicionarUsuario(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Usuário cadastrado com sucesso", resultado);
        }

        [Fact]
        public async void AdicionarCliente_RetornaErroPorValidacao()
        {
            // Arrange
            var usuario = new UsuarioDTO();
            var service = new Mock<IUsuarioService>();
            service.Setup(p => p.AdicionarUsuario(It.IsAny<UsuarioDTO>())).ThrowsAsync(new InvalidOperationException("erro"));
            UsuarioAppService appService = new UsuarioAppService(service.Object);

            // Act
            string resultado = await appService.AdicionarUsuario(usuario);

            // Assert
                Assert.Equal("erro", resultado);
        }

        [Fact]
        public async void BuscarTodosOsUsuarios_RetornaListaDeTodosOsUsuarios()
        {
            // Arrange
            var lista = new List<UsuarioGetDTO>() as IEnumerable<UsuarioGetDTO>;
            var service = new Mock<IUsuarioService>();
            service.Setup(p => p.BuscarTodosOsUsuarios()).ReturnsAsync(lista);
            var appService = new UsuarioAppService(service.Object);

            // Act
            var resultado = await appService.BuscarrTodosOsUsuarios();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async void BuscarUsuarioPorID_RetornaUsuarioCorrespondente()
        {
            // Arrange
            var id = Guid.NewGuid();
            var usuario = new UsuarioGetDTO() { Id = id};
            var service = new Mock<IUsuarioService>();
            service.Setup(p => p.BuscarUsuarioPorId(id)).ReturnsAsync(usuario);
            var appService = new  UsuarioAppService(service.Object);

            // Act
            var resultado = await appService.BuscarUsuarioPorId(id);

            // Assert 
            Assert.NotNull(resultado);
            Assert.Equal(usuario, resultado);
        }
    }
}
