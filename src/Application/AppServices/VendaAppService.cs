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
        private readonly IVendaService _ServiceVenda;
        private readonly IProdutoService _ServiceProduto;
        private readonly IClienteService _ServiceCliente;

        public VendaAppService(IVendaService servicev, IProdutoService servicep, IClienteService servicec)
        {
            _ServiceVenda = servicev;
            _ServiceProduto = servicep;
            _ServiceCliente = servicec;
        }

        public async Task<IEnumerable<Venda>> BuscarTodasAsVendas()
        {
            return await _ServiceVenda.BuscarTodasAsVendas();
        }

        public async Task<Venda> BuscarVendaPorId(Guid id)
        {
            return await _ServiceVenda.BuscarVendaPorId(id);
        }

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
            var valor = await CalcularValorDaVenda(vendadto);
            return await _ServiceVenda.AdicionarVenda(vendadto, valor);
        }

        public async Task<double> CalcularValorDaVenda(VendaDTO vendadto)
        {
            double valor = 0;

            foreach (ProdutoVendaDTO produto in vendadto.ListaProdutos)
            {
                var produtoCompleto = await _ServiceProduto.BuscarProdutoPorId(produto.Id);
                valor += (produto.Quantidade * produtoCompleto.Valor);
            }
            return valor;
        }

        public async Task ValidarVenda(VendaDTO vendadto)
        {
            if (await _ServiceCliente.BuscarClientePorCPF(vendadto.CPF) == null) { throw new Exception("Cliente não cadastrado"); }

            foreach (ProdutoVendaDTO item in vendadto.ListaProdutos)
            {
                var produto = await _ServiceProduto.BuscarProdutoPorId(item.Id);
                if (produto == null) { throw new Exception("Não existe produto com o id informado:" + item.Id.ToString()); }
                if (item.Quantidade > produto.Quantidade) { throw new Exception("Quantidade desejada superior ao disponível do produto " + item.Id.ToString()); }
            }
        }

        public async Task<string> AtualizarQuantidadeDeProdutos(VendaDTO vendadto)
        {
            foreach (ProdutoVendaDTO item in vendadto.ListaProdutos)
            {
                var produto = await _ServiceProduto.BuscarProdutoPorId(item.Id);
                produto.Quantidade -= item.Quantidade;
                await _ServiceProduto.AtualizarProduto(produto);
            }
            return "Quantidades atualizadas com sucesso";
        }


    }
}
