using System;
using SeeSharpShop.UnitTests.Factories;
using NUnit.Framework;
using SeeSharpShop.Repositories;
using SeeSharpShop.Services;
using SeeSharpShop.Validators;
using SeeSharpShop.Models;
using FakeItEasy;

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
        public void ValidProduct_ReturnsTrue()
        {
            var product = ProductFactory.CreateSingle(1);
            var validator = new ProductValidator(productRepository);

            var results = validator.Validate(product);

            Assert.That(results.IsValid);
        }

    }
}
