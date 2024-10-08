using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateDto product)
        {
            _service.Add(product);
            return Ok("Product added succesfully");

        }
    }
}
