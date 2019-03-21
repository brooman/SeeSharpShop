using System;
using NUnit.Framework;
using SeeSharpShop.Repositories;
using SeeSharpShop.Services;
using FakeItEasy;
using SeeSharpShop.Models;
using SeeSharpShop.UnitTests.Factories;
using System.Collections.Generic;

namespace SeeSharpShop.UnitTests.Services
{
    [TestFixture]
    public class ProductServicesTest
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
        public void All_ReturnsResultFromRepository()
        {
            var products = ProductFactory.CreateCollection(10);
            A.CallTo(() => this.productRepository.All()).Returns(products);

            var result = this.productService.All();

            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Product));
            Assert.AreEqual(result, products);
        }

        [Test]
        public void Single_ReturnsResultFromRepository()
        {
            var product = ProductFactory.CreateSingle(1);
            A.CallTo(() => this.productRepository.Single(1)).Returns(product);

            var result = this.productService.Single(1);

            Assert.AreEqual(result.GetType(), typeof(Product));
            Assert.AreEqual(result, product);
        }
    }
}