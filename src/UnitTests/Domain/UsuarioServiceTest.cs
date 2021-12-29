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
            var c = new Usuario() { Nome = "Michael", CPF = "734.408.754-58", Email = "michael@.com" };
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
        [Fact]
        public async void ValidarCadastro_RetornaTrueCasoExistaEntidadeComMesmoEmailNoBanco()
        {
            // Arrange
            var usuario = new Usuario() { Nome = "Michael", CPF = "13602151662", Email = "michael@.com" };
            var usuarioerrado = new UsuarioDTO() { Nome = "Michael", CPF = "13602151662", Email = "michael@.com" };
            IEnumerable<Usuario> usuarios = new List<Usuario>() { usuario } as IEnumerable<Usuario>;
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(usuarios);
            var service = new UsuarioService(repository.Object);

            // Act
            bool resultado = await service.ValidarCadastro(usuarioerrado);

            // Assert
            Assert.True(resultado, "Mesmo com o email igual a outro objeto armazenado o retorno é falso");
        }

        [Fact]
        public async void ValidarCadastro_RetornaTrueParaObjetoComMesmoCPFExistenteNoBanco()
        {
            // Arrange
            var usuario = new Usuario() { Nome = "Michael", CPF = "13602151662", Email = "1michael@.com" };
            var usuarioerrado = new UsuarioDTO() { Nome = "Michael", CPF = "13602151662", Email = "Michael@.com" };
            IEnumerable<Usuario> usuarios = new List<Usuario>() { usuario } as IEnumerable<Usuario>;
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(usuarios);
            var service = new UsuarioService(repository.Object);

            // Act
            bool resultado = await service.ValidarCadastro(usuarioerrado);

            // Assert
            Assert.True(resultado, "Mesmo com usuário com CPF idêntico cadastrado o retorno está falso");
        }

        [Fact]
        public async Task ValidarTodasAsRegras_LancaExceptionParaErrosDeValidacaoDeCPF()
        {
            // Arrange
            Exception exTeste = null;
            var usuario = new UsuarioDTO() { CPF = "198.408.843-29" };// Final 8 é cpf válido
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Get());
            var service = new UsuarioService(repository.Object);

            // Act
            bool result1 = await service.ValidarCadastro(usuario);
            bool result2 = service.ValidarCPF(usuario.CPF);
            try
            {
                await service.ValidarTodasAsRegras(usuario);
            }
            catch (Exception ex)
            {
                exTeste = ex;
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.ValidarTodasAsRegras(usuario));
            Assert.NotNull(exTeste);
        }

        [Fact]
        public async Task ValidarTodasAsRegras_LancaExceptionParaErrosDeValidacaodeCadastro()
        {
            // Arrange
            var usuarioteste = new UsuarioDTO() { Nome = "Michael", CPF = "198.408.843-28", Email = "michael@.com" };
            var usuario = new Usuario() { Nome = "Michael", CPF = "198.408.843-28", Email = "michael@.com" };
            IEnumerable<Usuario> lista = new List<Usuario>() { usuario } as IEnumerable<Usuario>;
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new UsuarioService(repository.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.ValidarTodasAsRegras(usuarioteste));
        }

        [Fact]
        public async void AdicionarUsuario_RetornaMensagemDeSucessoParaValoresCorretos()
        {
            // Arrange
            var usuario = new UsuarioDTO() { CPF = "364.418.010-51", Tipo = "cliente", Email = "Michael@.com", Senha = "12345" };
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Add(It.IsAny<Usuario>())).ReturnsAsync("Usuário cadastrado com sucesso");
            var service = new UsuarioService(repository.Object);

            // Act
            string resultado = await service.AdicionarUsuario(usuario);
            // Assert
            Assert.Equal("Usuário cadastrado com sucesso", resultado);
        }

        [Fact]
        public async void BuscarTodosOsUsuarios_RetornaListaNulaDeUsuarios()
        {
            // Arrange
            var listaVazia = new List<Usuario>() as IEnumerable<Usuario>;
            var listaEsperada = new List<UsuarioGetDTO>() as IEnumerable<UsuarioGetDTO>;
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(listaVazia);
            var service = new UsuarioService(repository.Object);

            // Act
            IEnumerable<UsuarioGetDTO> resultado = await service.BuscarTodosOsUsuarios();

            // Assert
            Assert.Equal(listaEsperada, resultado);
        }

        [Fact]
        public async void BuscarTodosOsUsuarios_RetornaAListaDeUsuariosNoBanco()
        {
            // Arrange
            var c1 = new Usuario() { };
            var e1 = new UsuarioGetDTO() { };
            var lista = new List<Usuario>() { c1 } as IEnumerable<Usuario>;
            var listaEsperada = new List<UsuarioGetDTO>() { e1 };
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(p => p.Get()).ReturnsAsync(lista);
            var service = new UsuarioService(repository.Object);

            // Act
            List<UsuarioGetDTO> resultado = await service.BuscarTodosOsUsuarios() as List<UsuarioGetDTO>;

            // Assert
                        //Assert.Equal(listaEsperada[0], resultado[0]);
            Assert.NotNull(resultado);
        }

        [Theory]
        [InlineData("cliente")]
        [InlineData("administrador")]
        public void ValidarTipoDeUsuario_RetornaFalsoParaEntradaValida(string tipo)
        {
            // Arrange
            var usuario = new UsuarioDTO() { Tipo = tipo };
            var repository = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repository.Object);

            // Act
            var resultado = service.ValidarTipoDeUsuario(usuario);

            // Assert
            Assert.False(resultado);
        }

        [Theory]
        [InlineData("outro tipo")]
        [InlineData("")]
        public void ValidarTipoDeUsuario_RetornoVerdadeiroParaEntradasIncorretasDeTipo(string tipo)
        {
            // Arrange
            var usuario = new UsuarioDTO() { Tipo = tipo };
            var repository = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repository.Object);

            // Act
            var resultado = service.ValidarTipoDeUsuario(usuario);

            // Assert
            Assert.True(resultado);
        }


    }// fim da classe
}
