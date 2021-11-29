using System;

namespace Domain.Interfaces.Entities
{
    public interface ICliente
    {
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
    }
}
