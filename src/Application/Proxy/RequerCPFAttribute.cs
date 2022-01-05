using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Proxy
{

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RequerCPFAttribute : Attribute
    {
        public string CPF { get; set; }
        public int? Posicao { get; set; }

        public RequerCPFAttribute(string cpf = null)
        {
            this.CPF = cpf;
        }
        public RequerCPFAttribute(int posicao , string cpf = null)
        {
            this.Posicao = posicao;
            this.CPF = cpf;
        }
    }
}
