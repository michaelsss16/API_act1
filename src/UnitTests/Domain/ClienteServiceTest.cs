using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace UnitTests.Domain
{
    public class ClienteServiceTest
    {
        [Fact]
        public async void ValidarCadastro_RetornarFalsoParaAusênciaDeDadosNoRepositorio()
        {
            // Arrange
            Cliente c = new Cliente();
            var repositoryMock = new Mock<IClienteRepository>();
            var service = new ClienteService(repositoryMock.Object);

            // Act
            var Result = await service.ValidarCadastro(c);

            //Assert
            Assert.False(Result, "Entradas nulas não estão sendo validadas");
        }

        [Fact]
        public void ValidarCadastro_RetornaPositivoParaEntradaComMesmoClienteNoBanco()
        {
            // Arrange
            Cliente c = new Cliente() { Nome = "Michael", CPF = "734.408.754-58", Email = "michael@.com" };
            List<Cliente> responseMockList = new List<Cliente>();
            responseMockList.Add(c);
            IEnumerable<Cliente> responseMock = responseMockList as IEnumerable<Cliente>;
            var repositoryMock = new Mock<IClienteRepository>();
            repositoryMock.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(responseMock);
            ClienteService service = new ClienteService(repositoryMock.Object);

            // Act
            bool resultado = service.ValidarCadastro(c).Result;

            // Assert
            Assert.True(resultado, "O retorno está falso mesmo com a entrada com entidade já presente no banco");
        }

        [Fact]
        public void ValidarCadastro_RetornaTrueCasoExistaEntidadeComMesmoEmailNoBanco()
        {
            // Arrange
            Cliente cliente = new Cliente() {Nome = "Michael", CPF="13602151662", Email="michael@.com" };
            Cliente clienteErrado = new Cliente() {Nome = "Michael", CPF="13602151663", Email="michael@.com" };
            IEnumerable<Cliente> clientes = new List<Cliente>() {cliente } as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(clientes);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            bool resultado = service.ValidarCadastro(clienteErrado).Result;

            // Assert
            Assert.True(resultado, "Mesmo com o email igual a outro objeto armazenado o retorno é falso");
        }

        [Fact]
        public void ValidarCadastro_RetornaTrueParaObjetoComMesmoCPFExistenteNoBanco()
        {
            // Arrange
            Cliente cliente = new Cliente() { Nome = "Michael", CPF = "13602151662", Email = "michael@.com" };
            Cliente clienteErrado = new Cliente() { Nome = "Michael", CPF = "13602151662", Email = "Michael@.com" };
            IEnumerable<Cliente> clientes = new List<Cliente>() { cliente } as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(clientes);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            bool resultado = service.ValidarCadastro(clienteErrado).Result;

            // Assert
            Assert.True(resultado, "Mesmo com cliente com mesmo CPF cadastrado o retorno está falso");
        }




    }
}
