using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.DTO;
using Domain.Interfaces.Services;
using Application.Proxy;

namespace Application.AppServices
{
    public class VendaAppService : IVendaAppService
    {
        private readonly IVendaService _ServiceVenda;
        private readonly IProdutoService _ServiceProduto;
        private readonly IClienteService _ServiceCliente;
        private readonly ICupomService _ServiceCupom;

        public VendaAppService(IVendaService servicev, IProdutoService servicep, IClienteService servicec, ICupomService serviceCupom)
        {
            _ServiceVenda = servicev;
            _ServiceProduto = servicep;
            _ServiceCliente = servicec;
            _ServiceCupom = serviceCupom;
        }

        public async Task<IEnumerable<Venda>> BuscarTodasAsVendas()
        {
            return await _ServiceVenda.BuscarTodasAsVendas();
        }

        public async Task<Venda> BuscarVendaPorId(Guid id)
        {
            return await _ServiceVenda.BuscarVendaPorId(id);
        }

        [RequerCPF]
        public async Task<IEnumerable<Venda>> BuscarVendasPorCPF(string cpf)
        {
            var Result = await _ServiceVenda.BuscarVendasPorCPF(cpf);
            return Result;
        }

        public async Task<string> AdicionarVenda(VendaDTO vendadto)
        {
            try { await ValidarVenda(vendadto); }
            catch (Exception E) { return E.Message; }
            await AtualizarQuantidadeDeProdutos(vendadto);
            var valor = await CalcularValorDaVenda(vendadto); //todo: Calcular o desconto
            var porcentagemDesconto = await BuscarPorcentagemDeDesconto(vendadto);
            return await _ServiceVenda.AdicionarVenda(vendadto, valor, porcentagemDesconto);
        }

        public async Task<double> CalcularValorDaVenda(VendaDTO vendadto)
        {
            var ListaIds = vendadto.ListaProdutos.Select(produto => produto.ProdutoId).ToList();
            var ListaProdutos = await _ServiceProduto.BuscarListaDeProdutosPorId(ListaIds);
            return _ServiceVenda.CalcularValorDaVenda(vendadto, ListaProdutos);
        }

        public async Task ValidarVenda(VendaDTO vendadto)
        {
            await _ServiceCliente.BuscarClientePorCPF(vendadto.CPF);
            await _ServiceProduto.ValidarVenda(vendadto.ListaProdutos);
        }

        public async Task<string> AtualizarQuantidadeDeProdutos(VendaDTO vendadto)
        {
            var Result = await _ServiceProduto.AtualizarListaDeProdutos(vendadto.ListaProdutos);
            return Result;
        }

        //todo: adicionar o o métdoto na interface 
        public async Task<double> BuscarPorcentagemDeDesconto(VendaDTO vendadto)
        {
            if (vendadto.CupomId == Guid.Empty) { return 0.0; }
            else {
                var cupom = await _ServiceCupom.BuscarCupomPorId(vendadto.CupomId);
                return cupom.Porcentagem;
            }
        }
    }
}
