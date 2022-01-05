// todo: Proxy não está finalizado e sem definição das funcionalidades necessárias. O arquivo de ocnfiguração também não é presente.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Proxy
{
    public class Proxy<T>
    {
        private T _obj;

        public Proxy(T obj)
        {
            _obj = obj;
        }

    }
}
