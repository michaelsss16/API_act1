using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICupomService
    {
        public Task<List<Cupom>> BuscarTodosOsCupons();
        public Task<Cupom> BuscarCupomPorId(Guid guid);
        public Task<string> AdicionarCupom(Cupom cupom);
        // todo: Adicionar serviços de validação do cupom  durante inserção 
    }
}
