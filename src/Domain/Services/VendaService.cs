using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;

namespace Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _Repository;
        public VendaService(IVendaRepository repository)
        {
            _Repository = repository;
        }

        public async Task<IEnumerable<Venda>> BuscarTodasAsVendas()
        {
            return await _Repository.Get();
        }

        public async Task<Venda> BuscarVendaPorId(Guid id)
        {
            return await _Repository.Get(id);
        }

        public async Task<string> AdicionarVenda(VendaDTO vendadto) {
            var venda = new Venda() { Guids = vendadto.Guids, Quantidades = vendadto.Quantidades, CPF = vendadto.CPF, Id = Guid.NewGuid()};
            return await _Repository.Add(venda);
        }

    }
}
