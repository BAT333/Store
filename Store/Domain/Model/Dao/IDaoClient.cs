using Store.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Model.Dao
{
    internal interface IDaoClient<T> where T : class
    {
        T Add(T entity);
        bool Delete(int id,int idAddress);
        T? Update(int id, T entity);
        T? GetById(int id);
    }
}
