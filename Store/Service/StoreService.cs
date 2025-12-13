

using Store.Domain;
using Store.Infrastructure.ExceptionCustomized;
using Store.Domain.Model.Dao;
using Store.Domain.Model.Service;


namespace Store.Service
{
    internal class StoreService : IServiceStore<Cart>
    {
        private readonly IDaoStore<Cart> _storeRepository;
        private readonly IDaoClient<Client> _clientRepository;
        private readonly IDaoProduct<Product> _productRepository;

        public StoreService(IDaoStore<Cart> storeRepository, IDaoClient<Client> clientRepository, IDaoProduct<Product> productRepository)
        {
            this._storeRepository = storeRepository;
            this._clientRepository = clientRepository;
            this._productRepository = productRepository;
        }

        public Cart Add(Cart entity)
        {
            if (entity == null) throw new ExceptionalStore("Store not registered.");
            Client? client = this._clientRepository.GetById(entity.IdClient);
            Product? product = this._productRepository.GetById(entity.IdProduct);

            return this._storeRepository.Add(new Cart(client.Id, product.Id));
        }

        public bool Delete(int id)
        {
            Cart? cart = this.GetById(id);
            return this._storeRepository.Delete(id);
        }

        public Cart? GetById(int id)
        {
            Cart? cart = this._storeRepository.GetById(id);
            if (cart == null || cart.Id <= 0) throw new ExceptionalStore("Store not registered");
            return cart == null ? null : cart;
        }

        public Cart? Update(int id, Cart entity)
        {
            
            this.GetById(id);

            if(entity == null) throw new ExceptionalStore("Store not registered");

            Client? client = this._clientRepository.GetById(entity.IdClient);
            Product? product = this._productRepository.GetById(entity.IdProduct);

            return this._storeRepository.Update(id, entity); ; 

        }
    }
}
