using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;
using SeeSharpShop.Services;
using Newtonsoft.Json.Linq;

namespace SeeSharpShop.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrderController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            this.orderService = new OrderService(new OrderRepository(connectionString));
        }

        [HttpGet("{key}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string key)
        {
            try
            {
                var order = orderService.Get(key);
                return Ok(order);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody]JObject data)
        {
            try
            {
                var customer = new Customer
                {
                    Name = (string)data["customer"]["name"],
                    Adress = (string)data["customer"]["adress"],
                    Zipcode = (string)data["customer"]["zip"],
                };

                var products = data["products"].ToObject<List<int>>();

                string key = orderService.Create(customer, products);

                return Ok(key);

            } catch (Exception){
                return BadRequest("Malformed json");
            }
        }

    }
}
