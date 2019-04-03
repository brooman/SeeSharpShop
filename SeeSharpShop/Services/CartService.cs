using System;
using System.Collections.Generic;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;

namespace SeeSharpShop.Services
{
    public class CartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public List<Product> Get(string Key)
        {
            return this.cartRepository.Get(Key);
        }

        public string UpdateOrCreate(string Key, List<int> Products)
        {
            return this.cartRepository.UpdateOrCreate(Key, Products);
        }
    }
}
