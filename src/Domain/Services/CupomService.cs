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
            cupom.Id = Guid.NewGuid();
            ValidarCupom(cupom);

            return await _repository.Add(cupom);
        }

        // todo: adicionar serviço de validação para o cupom 
        public void ValidarCupom(Cupom cupom)
        {
            if (String.IsNullOrEmpty(cupom.Nome)) { throw new Exception("Erro: O cupom deve possuir nome válido"); }
            if ((cupom.Porcentagem <= 0.0) || (cupom.Porcentagem >= 100.0)) { throw new Exception("Erro: O cupom deve possuir porcentagem de desconto entre 0% e 100%"); }
        }
    }
}
