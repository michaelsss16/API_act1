using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CupomService : ICupomService
    {
        public readonly ICupomRepository _repository;

        public CupomService(ICupomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cupom>> BuscarTodosOsCupons()
        {
            return await _repository.Get();
        }

        public async Task<Cupom> BuscarCupomPorId(Guid guid)
        {
            return await _repository.Get(guid);
        }

        public async Task<string> AdicionarCupom(Cupom cupom)
        {
            return await _repository.Add(cupom);
        }

        // todo: adicionar serviço de validação para o cupom 
    }
}
