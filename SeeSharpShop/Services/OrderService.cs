using System;
using System.Collections.Generic;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;

namespace SeeSharpShop.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Order Get(string Key)
        {
            return this.orderRepository.Get(Key);
        }

        public string Create(Customer customer, List<int> products)
        {

            return orderRepository.Create(customer, products);
        }

    }
}
