using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;
using SeeSharpShop.Services;
using System.Linq;

namespace SeeSharpShop.Controllers
{

    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductService productService;

        public ProductsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this.productService = new ProductService(new ProductRepository(connectionString));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            var products = productService.All();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Show(int id)
        {
            try
            {
                var product = productService.Get(id);
                return Ok(product);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        public IActionResult Store([FromBody] Product product)
        {
            //Add and Return OK if valid
            var result = productService.Add(product);

            return result == null ? Ok() : (IActionResult)BadRequest(new { Error = result });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var result = productService.Update(id, product);

            return result == null ? Ok() : (IActionResult)BadRequest(new { Error = result });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        public IActionResult Destroy(int id)
        {
            var result = productService.Delete(id);

            return result == null ? Ok() : (IActionResult)BadRequest(new { Error = result } );
        }
    }
}
