using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain
{
    public class ValidacoesServiceTest
    {
        [Theory]
        [InlineData("266.990.198-06")]
        [InlineData("26699019806")]
        [InlineData("26699019805*")]
        [InlineData("266990198053")]

        public void ValidarCpf_DeveRetornarValorFalsoParaEntradasIncorretas(string cpf)
        {
            // Arrange
            var service = new ValidacoesService();

            // Act
            bool Resultado = service.ValidarCPF(cpf);

            // Assert
            Assert.False(Resultado, "O valor de retorno está verdadeiro para entradas incorretas de CPF");
        }

        [Theory]
        [InlineData("266.990.198-05")]
        [InlineData("26699019805")]
        public void ValidarCPF_DeveRetornarPositivoParaDadosDeEntradaCorretos(string cpf)
        {
            // Arrange
            var service = new ValidacoesService();

            // Act
            bool Resultado = service.ValidarCPF(cpf);

            // Assert
            Assert.True(Resultado, "O valor de retorno está falso para entradas corretas de CPF");
        }

    }
}
