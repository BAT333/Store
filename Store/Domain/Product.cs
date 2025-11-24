
namespace Store.Domain
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, string description, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
    }
}
