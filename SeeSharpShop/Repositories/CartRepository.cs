using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using SeeSharpShop.Models;

namespace SeeSharpShop.Repositories
{
    public class CartRepository : ICartRepository
    {
        protected readonly string connectionString;

        public CartRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> Get(string key)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<Product>("SELECT Products.* FROM Products " +
                	"INNER JOIN Cart_Products ON  Cart_Products.product_id = Products.id " +
                	"INNER JOIN Cart ON Cart_products.cart_id = Cart.id AND Cart.cart_key = @Key",
                    new { Key = key })
                    .ToList();
            }
        }

        public string UpdateOrCreate(string key, List<int> products)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                //Generate unique identifier key
                if (key == string.Empty || !this.CheckKey(key) )
                {
                    key = Guid.NewGuid().ToString("N");
                    connection.Execute("INSERT INTO Cart (cart_key) VALUES (@Key)", new { Key = key });
                }

                var cart_id = connection.Query<String>("SELECT id FROM Cart WHERE cart_key = @Key", new { Key = key });

                //Bad practice :)
                connection.Execute("DELETE FROM Cart_Products WHERE cart_id = @cart_id", new { cart_id });

                foreach (int product_id in products) {
                    connection.Execute("INSERT INTO Cart_Products (cart_id, product_id) VALUES (@Cart_id, @Product_id);", new { Cart_id = cart_id, Product_id = product_id });
                }

                return key;
            }
        }

        public bool CheckKey(string key)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<String>("SELECT id FROM Cart WHERE cart_key = @Key", new { Key = key }).Any();
            }
        }
    }
}
