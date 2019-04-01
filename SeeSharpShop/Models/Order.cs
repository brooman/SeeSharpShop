using System;
using System.Collections.Generic;

namespace SeeSharpShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Products { get; set; }
    }
}
