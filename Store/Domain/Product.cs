
using Store.Infrastructure.ExceptionCustomized;

namespace Store.Domain
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private double _price;

        public Product(int id, string name, string description, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
        public Product(string name, string description, double price)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public double Price
        {
            get { return this._price; }
            set
            {
                if (value > 1.0)
                {
                    _price = value;
                }
                else
                {
                    throw new ExceptionalProduct(message:"Invalid phone number price",innerException: new ArgumentException());
                }
            }

        }
    }
}
