using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces.Services;


namespace Application.AppServices
{
    public class VendaAppService : IVendaAppService
    {
        private readonly IVendaService _Service;

        public VendaAppService(IVendaService service)
        {
            _Service = service;
        }
        public async Task<IEnumerable<Venda>> BuscarTodasAsVendas()
        {
            return await _Service.BuscarTodasAsVendas();
        }

        public async Task<Venda> BuscarVendaPorId(Guid id)
        {
            return await _Service.BuscarVendaPorId(id);
        }

        public async Task<string> AdicionarVenda(VendaDTO vendadto)
        {
            try
            {
                return await _Service.AdicionarVenda(vendadto);
            }
            catch (Exception E)
            {
                return E.Message;
            }
        }
    }
}
