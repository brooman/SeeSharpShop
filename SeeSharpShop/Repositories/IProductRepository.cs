using System;
using System.Collections.Generic;
using SeeSharpShop.Models;

namespace SeeSharpShop.Repositories
{
    public interface IProductRepository
    {
        List<Product> All();
        Product Single(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        bool Exists(int id);
    }
}
