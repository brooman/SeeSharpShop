using System;
using System.Collections.Generic;
using SeeSharpShop.Models;

namespace SeeSharpShop.Repositories
{
    public interface ICartRepository
    {
        string UpdateOrCreate(string key, List<int> products);
        List<Product> Get(string key);
        bool CheckKey(string key);
    }
}
