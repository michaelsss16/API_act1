using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ICupomRepository
    {
        public Task<List<Cupom>> Get();
        public Task<Cupom> Get(Guid guid);
        public Task<string> Add(Cupom cupom);
    }
}
