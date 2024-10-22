using Application.Interfaces;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        private bool IsUserInRol(string rol)
        {
            var claimsRol = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
            return claimsRol is not null && claimsRol.Value == rol;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if(IsUserInRol("Client") || IsUserInRol("Admin"))
            {
                return Ok(_service.GetAll());
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateDto product)
        {
           if(IsUserInRol("Admin"))
            {
                _service.Add(product);
                return Ok("Product added succesfully");
            }
            return Forbid();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateDto productUpdate)
        {
            var product = _service.Get(id);
            if(product is null)
            {
                return NotFound($"No se encontro el producto con el ID: {id}");
            }

            if (IsUserInRol("Admin"))
            {
                _service.Update(id, productUpdate);
                return Ok("Product modified succesfully");
            }
            return Forbid();
        }
        

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (IsUserInRol("Admin"))
            {
                var existingProduct = _service.Get(id);
                if (existingProduct == null)
                {
                    return NotFound($"No se encontró ningún Producto con el ID: {id}");
                }
                _service.Delete(id);
                return Ok($"Producto con ID: {id} eliminado");
            }
            return Forbid();
        }

        
        [HttpGet("CheckStock")]
        public IActionResult CheckStock([FromQuery] int productId)
        {
            if (IsUserInRol("Client") || IsUserInRol("Admin"))
            {
                var stockCheckResult = _service.CheckStock(productId);
                var product = _service.Get(productId);

                var message = "";
                
                if (stockCheckResult == 0)
                {
                   message = $"{product.Stock} unidades disponibles.";
                }
                else
                {
                    message = "Agotado";
                }
                return Ok(message);
            }

            return Forbid();
        }
    }
}
