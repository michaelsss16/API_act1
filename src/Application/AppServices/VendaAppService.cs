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

        public async Task<string> AdicionarVenda(VendaDTO vendadto)
        {
            try { await ValidarVenda(vendadto); }
            catch (Exception E) { return E.Message; }
            await AtualizarQuantidadeDeProdutos(vendadto);
            return await _ServiceVenda.AdicionarVenda(vendadto);
        }

        public async Task ValidarVenda(VendaDTO vendadto)
        {
            int QuantidadeDeItens = vendadto.Guids.Count();
            if (await _ServiceCliente.BuscarClientePorCPF(vendadto.CPF) == null) { throw new Exception("Cliente não cadastrado"); }
            for (int i = 0; i < QuantidadeDeItens; i++)
            {
                Produto produto = await _ServiceProduto.BuscarProdutoPorId(vendadto.Guids[i]);
                if (produto == null) { throw new Exception("Não existe produto com o id informado:"+ vendadto.Guids[i].ToString()); }
                if (vendadto.Quantidades[i] > produto.Quantidade) { throw new Exception("Quantidade desejada superior ao disponível do produto "+ vendadto.Guids[i].ToString()); }
            }
        }

        public async Task<string> AtualizarQuantidadeDeProdutos(VendaDTO vendadto)
        {
            int QuantidadeDeItens = vendadto.Guids.Count();
            for (int i = 0; i < QuantidadeDeItens; i++)
            {
                Produto produto = await _ServiceProduto.BuscarProdutoPorId(vendadto.Guids[i]);
                produto.Quantidade -= vendadto.Quantidades[i];
            }

            return "Quantidades atualizadas com sucesso";
        }

    }
}
