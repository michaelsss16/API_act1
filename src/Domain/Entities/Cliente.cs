using System;
using Domain.Interfaces.Entities;

namespace Domain.Entities
{
    public class Cliente : ICliente
    {
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
    }
}
