
namespace Store.Domain
{
    internal class Cart
    {
        public int Id { get; }
        public int IdClient { get; }
        public int IdProduct { get; }

        public Cart(int id, int idClient, int idProduct)
        {
            this.Id = id;
            this.IdClient = idClient;
            this.IdProduct = idProduct;
        }
    }
}
