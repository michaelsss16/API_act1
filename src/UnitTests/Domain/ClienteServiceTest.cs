﻿using System;
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
        public async void ValidarCadastro_DeveRetornarFalse()
        {
            Cliente c = new Cliente();
            var repositoryMock = new Mock<IClienteRepository>();
            var service = new ClienteService(repositoryMock.Object);
            //repositoryMock.Setup(p=>p.BuscarTodosOsClientes()).Returns(null);
            var Result = await service.ValidarCadastro(c);
            Assert.False(Result, "A validação está aceitando clientes nulos como entrada.");
        }
    }
}
