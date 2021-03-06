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

        public async Task<string> AdicionarVenda(VendaDTO vendadto, double valor, double porcentagem)
        {
            vendadto.CPF = Utils.Util.FormatarCPF(vendadto.CPF);
            var venda = new Venda() { ListaProdutos = vendadto.ListaProdutos, CPF = vendadto.CPF, Id = Guid.NewGuid(), Valor = valor , CupomId= vendadto.CupomId};
            //todo: modificar para um método auxiliar
            venda.PorcentagemDeDesconto = porcentagem;
            venda.ValorComDesconto = venda.Valor * (1 - porcentagem / 100);
            return await _Repository.Add(venda);
        }

        public async Task<IEnumerable<Venda>> BuscarVendasPorCPF(string cpf)
        {
            var ListaVendas = await _Repository.Get();
            List<Venda> Result = new List<Venda>();
            ListaVendas.ToList().ForEach(delegate (Venda venda)
            {
                if (venda.CPF == cpf) { Result.Add(venda); }
            });
            if (Result.Count() == 0) { throw new Exception("Não existem informações de venda para o CPF informado"); }
            return Result;
        }

        public double CalcularValorDaVenda(VendaDTO vendadto, List<Produto> produtos)
        {
            double ValorTotal = 0;
            for (int i = 0; i < vendadto.ListaProdutos.Count(); i++)
            {
                ValorTotal += (vendadto.ListaProdutos[i].Quantidade * produtos[i].Valor);
            }
            return ValorTotal;
        }
    }
}
