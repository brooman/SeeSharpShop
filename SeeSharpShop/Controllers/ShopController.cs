using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeeSharpShop.Models;

namespace SeeSharpShop.Controllers
{
    [Route("api/[controller]")]
    public class ShopController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            return Ok(Database);
        }

        private static readonly List<Product> Database = new List<Product> { 
            new Product
            {
                Id = 0,
                Name = "Glasses",
                Cost = 10
            },
            new Product
            {
                Id = 1,
                Name = "Cool Glasses",
                Cost = 10
            },
            new Product
            {
                Id = 2,
                Name = "Uncool Glasses",
                Cost = 10
            }
        };
    }
}
