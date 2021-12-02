using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private Dictionary<Guid, Venda> ListaVendas = new Dictionary<Guid, Venda>();

        public async Task<IEnumerable<Venda>> Get()
        {
            return await  Task.Run(() => ListaVendas.Values.ToList());
        }

        public async Task<Venda> Get(Guid id) {
            return await Task.Run(() => ListaVendas.GetValueOrDefault(id));
        }

        public async Task<string> Add(Venda venda) {
            await Task.Run(()=> ListaVendas.Add(venda.Id, venda));
            return "Venda adicionada com sucesso!";
        }

    }
}
