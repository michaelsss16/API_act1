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

        public Task ValidarVenda(VendaDTO vendadto);

        public Task<string> AtualizarQuantidadeDeProdutos(VendaDTO vendadto);

        public Task<IEnumerable<Venda>> BuscarVendasPorCPF(string cpf);

        public Task<double> CalcularValorDaVenda(VendaDTO vendadto);
    }
}
