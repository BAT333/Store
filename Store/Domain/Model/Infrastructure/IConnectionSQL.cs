using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Model.Infrastructure
{
    internal interface IConnectionSQL<T> where T : class
    {
        T CreateOpenConnection();
    }
}
