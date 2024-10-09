using Application.Interfaces;
using Application.Models;
using Application.Services;
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

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateDto productUpdate)
        {
            _service.Update(id, productUpdate);
            return Ok("Product modified succesfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var existingProduct = _service.Get(id);
            if (existingProduct == null)
            {
                return NotFound($"No se encontró ningún Producto con el ID: {id}");
            }
            _service.Delete(id);
            return Ok($"Producto con ID: {id} eliminado");
        }
    }
}
