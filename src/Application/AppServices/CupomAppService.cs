using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class CupomAppService : ICupomAppService
    {
        public readonly ICupomService _service;

        public CupomAppService(ICupomService service)
        {
            _service = service;
        }

        public async Task<List<Cupom>> BuscarTodosOsCupons()
        {
            return await _service.BuscarTodosOsCupons();
        }

        public async Task<Cupom> BuscarCupomPorId(Guid guid)
        {
            return await _service.BuscarCupomPorId(guid);
        }

        public async Task<string> AdicionarCupom(Cupom cupom)
        {
            try {
                return await _service.AdicionarCupom(cupom);
            } catch (Exception e) {
                return e.Message;
            }
            
        }

        // todo: adicionar serviço de validação para o cupom 
    }

}
