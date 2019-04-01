using System;
using System.Collections.Generic;
using SeeSharpShop.Models;

namespace SeeSharpShop.Repositories
{
    public interface IOrderRepository
    {
        string Create(Customer customer, List<int> Products);
        Order Get(string key);
        bool CheckKey(string key);
    }
}
