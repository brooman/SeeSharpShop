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
                "INSERT INTO Products (name, description, image_path, cost) VALUES(@Name, @Description, @ImagePath, @Cost)",
                    product
                );
            }
        }

        public void Update(Product product)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                    "UPDATE Products SET " +
                    "name = @Name, " +
                    "description = @Description, " +
                    "image_path = @ImagePath, " +
                    "cost = @Cost " +
                    "WHERE id = @Id",
                    product
                );
            }
        }

        public bool Exists(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var result = connection.Query<String>("SELECT id FROM Products WHERE id = @Id", new { Id = id });

                return result.Any();
            }
        }
    }
}
