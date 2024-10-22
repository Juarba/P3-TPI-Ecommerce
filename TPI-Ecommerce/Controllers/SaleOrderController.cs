using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _service;

        public SaleOrderController(ISaleOrderService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var saleOrder = _service.Get(id);
            if (saleOrder == null)
            {
                return NotFound();
            }
            return Ok(saleOrder);

        }

        [HttpGet("{clientId}")]
        public IActionResult GetAllByClient([FromRoute] int clientId)
        {
            var saleOrders = _service.GetAllByClient(clientId);
            return Ok(saleOrders);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderCreateDTO saleOrderCreate)
        {
            _service.Add(saleOrderCreate);
            return Ok("Creada la venta para el cliente");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_service.Get(id) is null)
                return NotFound("No se encontro la Orden");

            _service.Delete(id);
            return Ok("Orden eliminada exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] SaleOrderUpdateDTO saleOrderUpdate)
        {
            try
            {
                _service.Update(id, saleOrderUpdate);
                return Ok("Orden modificada exitosamente");
            }
            catch(NotAllowedException e)
            {
                return BadRequest("Objeto no encontrado");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocurrio un error inesperado: " +e.Message);
            }
        }
    }
}

