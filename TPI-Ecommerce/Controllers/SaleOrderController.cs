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
            if (saleOrder is null)
            {
                return NotFound($"No se encontró la venta con ID: {id}");
            }
            return Ok(saleOrder);
        }

        [HttpGet("{clientId}")]
        public IActionResult GetAllByClient([FromRoute] int clientId)
        {
            var saleOrders = _service.GetAllByClient(clientId);
            if (saleOrders.Count == 0 )
            {
                return NotFound($"No se encontró al cliente con ID: {clientId}");
            }
            return Ok(saleOrders);
        }

        [HttpPost]
        public IActionResult AddSaleOrder([FromBody] SaleOrderCreateDTO saleOrderCreate)
        {
            _service.Add(saleOrderCreate);
            return Ok("Creada la venta para el cliente");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder(int id)
        {
            var saleOrder = _service.Get(id);
            if (saleOrder is null)
                return NotFound($"No se encontró la venta con ID: {id}");

            _service.Delete(id);
            return Ok("Venta eliminada exitósamente");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder([FromRoute] int id, [FromBody] SaleOrderUpdateDTO saleOrderUpdate)
        {
            try
            {
                _service.Update(id, saleOrderUpdate);
                return Ok("Orden modificada exitosamente");
            }
            catch(NotFoundException e)
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

