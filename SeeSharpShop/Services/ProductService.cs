using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;
using SeeSharpShop.Validators;

namespace SeeSharpShop.Services
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> All()
        {
            return this.productRepository.All();
        }

        public Product Get(int id)
        {
            return this.productRepository.Single(id);
        }

        public object Add(Product product)
        {
            //Validate input
            var validator = new ProductValidator(productRepository);
            var results = validator.Validate(product);

            if (results.IsValid)
            {
                this.productRepository.Add(product);
                return null;
            }

            //Return Validation Error
            var firstError = results.Errors.First();
            return new
            {
                firstError.PropertyName,
                firstError.ErrorMessage
            };
        }

        public object Update(int id, Product product)
        {
            product.Id = id;

            //Validate input
            var validator = new ProductValidator(productRepository);
            var results = validator.Validate(product, ruleSet: "MustExist");

            if (results.IsValid)
            {
                this.productRepository.Update(product);
                return null;
            }

            //Return Validation Error
            var firstError = results.Errors.First();
            return new
            {
                firstError.PropertyName,
                firstError.ErrorMessage
            };
        }

        public object Delete(int id)
        {
            if (productRepository.Exists(id))
            {
                productRepository.Delete(id);
                return null;
            }

            return new
            {
                PropertyName = "Id",
                ErrorMessage = "Requested product doesn't exist"
            };
        }
    }
}
