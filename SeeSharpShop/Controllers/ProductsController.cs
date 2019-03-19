using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeeSharpShop.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace SeeSharpShop.Controllers
{

    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        protected readonly string connectionString;

        public ProductsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Index()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var Products = connection.Query<Product>("SELECT * FROM products").ToList();
                return Ok(Products);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Show(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(this.connectionString))
                {
                    var Product = connection.Query<Product>("SELECT * FROM products WHERE id = @id", new { id });
                    return Ok(Product);
                }
            }
            catch(InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
