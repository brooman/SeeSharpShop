using System;
using SeeSharpShop.UnitTests.Factories;
using NUnit.Framework;
using SeeSharpShop.Repositories;
using SeeSharpShop.Services;
using SeeSharpShop.Validators;
using SeeSharpShop.Models;
using FakeItEasy;
using FluentValidation;
using System.Linq;

namespace SeeSharpShop.UnitTests.Validators
{
    [TestFixture]
    public class ProductValidatorTest
    {
        private IProductRepository productRepository;
        private ProductService productService;

        [SetUp]
        public void SetUp()
        {
            this.productRepository = A.Fake<IProductRepository>();
            this.productService = new ProductService(this.productRepository);
        }

        [Test]
        public void ProductValidator_ValidProductReturnsIsValidTrue()
        {
            var product = ProductFactory.CreateSingle(1);
            var validator = new ProductValidator(productRepository);

            var results = validator.Validate(product);

            Assert.That(results.IsValid);
        }

        [Test]
        public void ProductValidator_InvalidProductReturnsErrorMessage()
        {
            var product = new Product{ };
            var validator = new ProductValidator(productRepository);
            var results = validator.Validate(product);

            var firstError = results.Errors.First().ErrorMessage;
            Assert.That(firstError.Length > 0);
        }
    }
}
