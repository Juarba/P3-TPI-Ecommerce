using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _service;

        public SaleOrderController(ISaleOrderService service)
        {
            _service = service;
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id) 
        {
            if (id <= 0) 
                return BadRequest("Id incorrecto");
            
            var product = _service.Get(id);

            if (product is null)
                return NotFound("Orden no encontrada");

            return Ok(product);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            return Ok(_service.GetAll());
        }

        [HttpPost("Add")]
        public IActionResult Post([FromBody] SaleOrderCreateDTO saleOrderCreate)
        {
            _service.Add(saleOrderCreate);
            return Ok("Orden agregada exitosamente");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id) 
        {
            if(_service.Get(id) is null)
                return NotFound("No se encontro la Orden");
            
            _service.Delete(id);
            return Ok("Orden eliminada exitosamente");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] SaleOrderUpdateDTO saleOrderUpdate)
        {
            try
            {
                _service.Update(id, saleOrderUpdate);
                return Ok("Orden modificada exitosamente");
            }
            catch 
            {
                return NotFound("Objeto no encontrado");
            }
        }
    }
}
