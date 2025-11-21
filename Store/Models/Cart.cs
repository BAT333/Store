using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    internal class Cart
    {
        private int id { get; }
        private int idClient { get; }
        private int idProduct { get; }

        public Cart(int id, int idClient, int idProduct)
        {
            this.id = id;
            this.idClient = idClient;
            this.idProduct = idProduct;
        }
    }
}
