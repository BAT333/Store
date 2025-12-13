using Store.Domain;
using Store.Infrastructure.ExceptionCustomized;
using Store.Domain.Model.Dao;
using Store.Domain.Model.Service;


namespace Store.Service
{
    internal class ProductService : IServiceProduct<Product>
    {
        private readonly IDaoProduct<Product> _productRepository;
        public ProductService(IDaoProduct<Product> productRepository)
        {
            this._productRepository = productRepository;
        }
        public Product Add(Product entity)
        {
            if (entity == null) throw new ExceptionalProduct("Product not registered.");
            return this._productRepository.Add(entity);
        }

        public bool Delete(int id)
        {
            this.GetById(id);
            return this._productRepository.Delete(id);
        }

        public Product? GetById(int id)
        {
            Product product = this._productRepository.GetById(id);
            if (product == null || product.Id <= 0) throw new ExceptionalCustomer("Product not registered");
            return product;
        }

        public Product? Update(int id, Product entity)
        {
            this.GetById(id);
            if (entity == null) throw new ExceptionalProduct("Product not registered.");
            return this._productRepository.Update(id, entity);
        }
    }
}
