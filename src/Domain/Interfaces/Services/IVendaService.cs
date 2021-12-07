using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces.Services
{
    public interface IVendaService
    {
        public Task<IEnumerable<Venda>> BuscarTodasAsVendas();

        public Task<Venda> BuscarVendaPorId(Guid id);
        
        public Task<string> AdicionarVenda(VendaDTO vendadto, double valor);

        public Task<IEnumerable<Venda>> BuscarVendasPorCPF(string cpf);

        public double CalcularValorDaVenda(VendaDTO vendadto, List<Produto> produtos);
    }
}
