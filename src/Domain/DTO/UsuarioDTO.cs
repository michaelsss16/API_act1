﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public enum UserType { cliente, administrador}

    public class UsuarioDTO
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Senha { get; set; }

        public UserType Tipo { set; get; }

    }
}