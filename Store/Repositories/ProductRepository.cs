using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories
{
    internal class ProductRepository : IDao<Product>
    {
        public Product Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product Update(int id, Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
