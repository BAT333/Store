using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Model.Service
{
    internal interface IServiceProduct<T> where T : class
    {
        T Add(T entity);
        bool Delete(int id);
        T? Update(int id, T entity);
        T? GetById(int id);
    }
}
