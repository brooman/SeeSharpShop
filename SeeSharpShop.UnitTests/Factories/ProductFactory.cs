using System;
using System.Collections.Generic;
using SeeSharpShop.Models;

namespace SeeSharpShop.UnitTests.Factories
{
    public static class ProductFactory
    {
        public static List<Product> CreateCollection(int Count = 1)
        {
            var products = new List<Product>();

            for(int i = 0; i < Count; i++)
            {
                products.Add(CreateSingle(i));
            }

            return products;
        }

        public static Product CreateSingle(int id = 1)
        {
            var random = new Random();

            return new Product
            {
                Id = id,
                Name = $"Test item {id}",
                Description = $"Test description",
                ImagePath = $"https://example.com/testimage.png",
                Cost = random.Next(1, 1000),

            };
        }
    }
}
