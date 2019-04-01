using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using SeeSharpShop.Models;

namespace SeeSharpShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        Order IOrderRepository.Get(string key)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var order = connection.Query(
                    "SELECT Orders.id as orderid, Orders.*, Customers.* FROM Orders " +
                    "INNER JOIN Customers ON Orders.customer_id = Customers.id " +
                    "WHERE order_key = @Key",
                    new { Key = key }
                ).First();

                //Theres probably better way to do this but I couldn't figure out how
                var orderItems = connection.Query(
                    "SELECT * FROM OrderItems WHERE order_id = 4",
                    new { OrderId = (int)order.orderid }
                ).ToList();

                var products = new List<OrderItem>();

                foreach(var item in orderItems)
                {
                    products.Add(
                        new OrderItem {
                         Name = item.name, Cost = item.cost
                        }
                    );
                }

                return new Order
                {
                    Key = key,
                    Customer = new Customer
                    {
                        Name = (string)order.Name,
                        Adress = (string)order.Adress,
                        Zipcode = (string)order.Zipcode
                    },
                    Products = products
                };
            }
        }

        public string Create(Customer customer, List<int> Products)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                //Create Customer
                var customer_id = connection.Query<int>(
                    "INSERT INTO Customers (Name, Adress, Zipcode) VALUES (@Name, @Adress, @Zip; SELECT LAST_INSERT_ID()",
                    new {
                        customer.Name,
                        customer.Adress,
                        customer.Zipcode
                    }
                ).Single();


                //Create Order
                string key = Guid.NewGuid().ToString("N");

                var order_id = connection.Query<int>(
                    "INSERT INTO Orders (order_key, customer_id) VALUES (@Key, @Customer); " +
                	"SELECT LAST_INSERT_ID()",
                new { 
                    Key = key, 
                    Customer = customer_id
                 })
                 .Single();
                 
                //Store products in OrderItems
                foreach (var id in Products)
                {
                    var Product = connection.QuerySingle<Product>("SELECT * FROM Products WHERE id = @Id", new { id });

                    connection.Execute("INSERT INTO OrderItems (order_id, name, cost) VALUES (@Id, @Name, @Cost)", 
                    new {
                        Id = order_id,
                        Product.Name,
                        Product.Cost
                    });
                }

                return key;
            }
        }

        public bool CheckKey(string key)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<String>("SELECT id FROM Orders WHERE order_key = @Key", new { Key = key }).Any();
            }
        }
    }
}
