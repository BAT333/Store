using Store.Domain;
using Store.Model;

namespace Store.Service
{
    internal class ProductService : IDao<Product>
    {
        private readonly IDao<Product> _productRepository;
        public ProductService(IDao<Product> productRepository)
        {
            this._productRepository = productRepository;
        }
        public Product Add(Product entity)
        {
            if (entity == null) return null;
            return this._productRepository.Add(entity);
        }

        public bool Delete(int id)
        {
            if (this.GetById(id) == null) return false;
            return this._productRepository.Delete(id);
        }

        public Product? GetById(int id)
        {
            return this._productRepository.GetById(id);
        }

        public Product? Update(int id, Product entity)
        {
            if (this.GetById(id) == null) return null;
            if (entity == null) return null;
            return this._productRepository.Update(id, entity);
        }
    }
}
