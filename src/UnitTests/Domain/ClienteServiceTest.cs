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
using Domain.Interfaces.Services;
using Infrastructure.Repositories;

namespace UnitTests.Domain
{
    public class ClienteServiceTest
    {

        // O método ValidarCadastro deve retornar true quando já existir um outro cliente no banco com mesmo email ou cpf
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
        public async void ValidarCadastro_RetornaPositivoParaEntradaComMesmoClienteNoBanco()
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
            bool resultado = await service.ValidarCadastro(c);

            // Assert
            Assert.True(resultado, "O retorno está falso mesmo com a entrada com entidade já presente no banco");
        }

        [Fact]
        public async void ValidarCadastro_RetornaTrueCasoExistaEntidadeComMesmoEmailNoBanco()
        {
            // Arrange
            Cliente cliente = new Cliente() { Nome = "Michael", CPF = "13602151662", Email = "michael@.com" };
            Cliente clienteErrado = new Cliente() { Nome = "Michael", CPF = "13602151663", Email = "michael@.com" };
            IEnumerable<Cliente> clientes = new List<Cliente>() { cliente } as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(clientes);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            bool resultado = await service.ValidarCadastro(clienteErrado);

            // Assert
            Assert.True(resultado, "Mesmo com o email igual a outro objeto armazenado o retorno é falso");
        }

        [Fact]
        public async void ValidarCadastro_RetornaTrueParaObjetoComMesmoCPFExistenteNoBanco()
        {
            // Arrange
            Cliente cliente = new Cliente() { Nome = "Michael", CPF = "13602151662", Email = "michael@.com" };
            Cliente clienteErrado = new Cliente() { Nome = "Michael", CPF = "13602151662", Email = "Michael@.com" };
            IEnumerable<Cliente> clientes = new List<Cliente>() { cliente } as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(clientes);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            bool resultado = await service.ValidarCadastro(clienteErrado);

            // Assert
            Assert.True(resultado, "Mesmo com cliente com CPF idêntico cadastrado o retorno está falso");
        }

        // O método  Validar CPF deve retornar positivo para CPFs válidos 
        [Theory]
        [InlineData("266.990.198-06")]
        [InlineData("26699019806")]
        [InlineData("26699019805*")]
        [InlineData("266990198053")]

        public void ValidarCpf_DeveRetornarValorFalsoParaEntradasIncorretas(string cpf)
        {
            // Arrange
            Cliente cliente = new Cliente() { CPF = cpf };
            var repository = new Mock<IClienteRepository>();
            ClienteService service = new ClienteService(repository.Object);

            // Act
            bool Resultado = service.ValidarCPF(cliente.CPF);

            // Assert
            Assert.False(Resultado, "O valor de retorno está verdadeiro para entradas incorretasde CPF");
        }

        [Theory]
        [InlineData("266.990.198-05")]
        [InlineData("26699019805")]
        public void ValidarCPF_DeveRetornarPositivoParaDadosDeEntradaCorretos(string cpf)
        {
            // Arrange
            Cliente cliente = new Cliente() { CPF = cpf };
            var repository = new Mock<IClienteRepository>();
            ClienteService service = new ClienteService(repository.Object);

            // Act
            bool Resultado = service.ValidarCPF(cliente.CPF);

            // Assert
            Assert.True(Resultado, "O valor de retorno está falso para entradas corretats de CPF");
        }

        // Validar todas as regras chama os dois métodos de validação para verificar se o cliente informado para o cadastro é factível
        [Fact]
        public async Task ValidarTodasAsRegras_LancaExceptionParaErrosDeValidacaoDeCPF()
        {
            // Arrange
            Exception exTeste = null;
            var cliente = new Cliente() { CPF = "198.408.843-29" };// Final 8 é cpf válido
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes());
            var service = new ClienteService(repository.Object);

            // Act
            bool result1 = await service.ValidarCadastro(cliente);
            bool result2 = service.ValidarCPF(cliente.CPF);
            try
            {
                await service.ValidarTodasAsRegras(cliente);
            }
            catch (Exception ex)
            {
                exTeste = ex;
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.ValidarTodasAsRegras(cliente));
            Assert.NotNull(exTeste);
        }

        [Fact]
        public async Task ValidarTodasAsRegras_LancaExceptionParaErrosDeValidacaodeCadastro()
        {
            // Arrange
            Cliente cliente = new Cliente() { Nome = "Michael", CPF = "198.408.843-28", Email = "michael@.com" };
            IEnumerable<Cliente> lista = new List<Cliente>() { cliente } as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(lista);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.ValidarTodasAsRegras(cliente));
        }

        [Fact]
        public async void CadastrarCliente_RetornaMensagemDeSucessoParaClienteCorretoo()
        {
            // Arrange
            Cliente cliente = new Cliente() { CPF = "11111111111" };
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.AdicionarCliente(cliente)).ReturnsAsync("Cliente cadastrado com sucesso");
            var service = new ClienteService(repository.Object);

            // Act
            string resultado = await service.CadastrarCliente(cliente);
            // Assert
            Assert.Equal("Cliente cadastrado com sucesso", resultado);
        }

        [Fact]
        public async void BuscarTodosOsClientes_DeveRetornarTodosOsClientesRetornadosPeloBancoCasoNull()
        {
            // Arrange
            IEnumerable<Cliente> listaVazia = new List<Cliente>() as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(listaVazia);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            var resultado = await service.BuscarTodosOsClientes();

            // Assert
            Assert.Equal(listaVazia, resultado);
        }

        [Fact]
        public async void BuscarTodosOsClientes_DeveRetornarAListaDeClientesNoRepositorio()
        {
            // Arrange
            Cliente c1 = new Cliente() { Nome = "cliente1" };
            Cliente c2 = new Cliente() { Nome = "cliente2" };
            IEnumerable<Cliente> lista = new List<Cliente>() { c1, c2 } as IEnumerable<Cliente>;
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscarTodosOsClientes()).ReturnsAsync(lista);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            var resultado = await service.BuscarTodosOsClientes();

            // Assert
            Assert.Equal(lista, resultado);
        }

        [Theory]
        [InlineData("198.408.843-28")]
        [InlineData("19840884328")]
        public async void BuscarClientePorCPF_RetornaOClienteQuandoPossuiRegistroNoBanco(string cpf)
        {
            // Arrange
            string cpfLimpo = cpf.Trim().Replace(".", "").Replace("-", "");
            Cliente cliente = new Cliente() { Nome = "Michael", CPF = "19840884328", Email = "michael@.com" };
            var repository = new Mock<IClienteRepository>();
            repository.Setup(p => p.BuscaClientePorCPF(cpf)).ReturnsAsync(cliente);
            repository.Setup(p => p.BuscaClientePorCPF(cpfLimpo)).ReturnsAsync(cliente);
            ClienteService service = new ClienteService(repository.Object);

            // Act
            Cliente resultado = await service.BuscarClientePorCPF(cpf);

            // Assert
            Assert.Equal(resultado, cliente);
        }

        [Fact]
        public async Task BuscarClientePorCPF_retornoDoRepositorioNuloGerandoExcecao()
        {
            // Arrange
            var repository = new Mock<IClienteRepository>();
            ClienteService service = new ClienteService(repository.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => service.BuscarClientePorCPF("11111111111"));
        }
    }
}
