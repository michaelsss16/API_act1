using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain
{
    public class UsuarioServiceTest
    {
        [Fact]
        public async void ValidarCadastro_RetornarFalsoParaAusênciaDeDadosNoRepositorio()
        {
            // Arrange
            var c = new UsuarioDTO();
            var repositoryMock = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repositoryMock.Object);

            // Act
            var Result = await service.ValidarCadastro(c);

            //Assert
            Assert.False(Result, "Entradas nulas não estão sendo validadas");
        }

        [Fact]
        public async void ValidarCadastro_RetornaPositivoParaCadastroDeUsuarioJaExistenteNoBanco()
        {
            // Arrange
            var cdto = new UsuarioDTO() { Nome = "Michael", CPF = "734.408.754-58", Email = "michael@.com" };
            var c= new Usuario() { Nome = "Michael", CPF = "734.408.754-58", Email = "michael@.com" };
            var responseMockList = new List<Usuario>();
            responseMockList.Add(c);
            IEnumerable<Usuario> responseMock = responseMockList as IEnumerable<Usuario>;
            var repositoryMock = new Mock<IUsuarioRepository>();
            repositoryMock.Setup(p => p.Get()).ReturnsAsync(responseMock);
            UsuarioService service = new UsuarioService(repositoryMock.Object);

            // Act
            bool resultado = await service.ValidarCadastro(cdto);

            // Assert
            Assert.True(resultado, "O retorno está falso mesmo com a entrada com entidade já presente no banco");
        }

    }// fim da classe
}
