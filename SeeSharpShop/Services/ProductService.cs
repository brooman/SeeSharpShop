using System.Collections.Generic;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;

namespace SeeSharpShop.Services
{
    public class ProductService
    {
        private readonly string connectionString;
        private readonly ProductRepository productRepository;

        public ProductService(ProductRepository newsRepository)
        {
            this.productRepository = newsRepository;
        }

        public List<Product> All()
        {
            return this.productRepository.All();
        }

        public Product Get(int id)
        {
            return this.productRepository.Single(id);
        }

        public bool Add(Product product)
        {
            this.productRepository.Add(product);
            return true;
        }
    }
}
