using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SeeSharpShop.Models;

namespace SeeSharpShop.Repositories
{
    public class ProductRepository
    {
        protected readonly string connectionString;
        
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> All()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<Product>("SELECT * FROM products").ToList();
            }
        }

        public Product Single(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM products WHERE id = @id", new { id });
            }
        }

        public void Add(Product product)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                "INSERT INTO Products (name, description, image_path, cost) VALUES(@Name, @Description, @ImagePath, @Cost);",
                    product
                );
            }
        }
    }
}
