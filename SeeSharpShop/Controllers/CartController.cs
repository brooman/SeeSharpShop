using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;
using SeeSharpShop.Services;

namespace SeeSharpShop.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ProductService productService;
        private readonly CartService cartService;

        public CartController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this.productService = new ProductService(new ProductRepository(connectionString));
            this.cartService = new CartService(new CartRepository(connectionString));
        }

        [HttpGet("{key}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Index(string Key)
        {
            var products = cartService.Get(Key);
            return Ok(products);
        }

        [HttpPost("{key}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateOrCreate(string Key, [FromBody]List<int> products)
        {
            string cart_key = cartService.UpdateOrCreate(Key, products);

            return Ok(new { Key = cart_key } );
        }
    }
}