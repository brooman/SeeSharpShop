using System;
using Microsoft.AspNetCore.Mvc;

namespace SeeSharpShop.Controllers
{
    [Route("api/[controller]")]
    public class ShopController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }
    }
}
