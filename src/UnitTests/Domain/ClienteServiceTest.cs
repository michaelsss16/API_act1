using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Domain.Entities;
using Domain.Services;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace UnitTests.Domain
{
    public class ClienteServiceTest
    {   
        [Fact]
        public async void ValidarCadastro_DeveRetornarFalse()
        {
            Cliente c = new Cliente();
            var repository = new ClienteRepository();
            var service = new ClienteService(repository);
            var Result = await service.ValidarCadastro(c);
            Assert.False(Result);
        }
    }
}
