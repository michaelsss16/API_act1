using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.DTO;
using Domain.Services;

namespace Application.Interfaces
{
    public interface IVendaAppService
    {
        public Task<IEnumerable<Venda>> BuscarTodasAsVendas();

        public Task<Venda> BuscarVendaPorId(Guid id);

        public Task<string> AdicionarVenda(VendaDTO vendadto);
    }
}
