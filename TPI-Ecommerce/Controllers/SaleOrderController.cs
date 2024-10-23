using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        private bool IsUserInRol(string rol)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role); // Obtener el claim de rol, si existe
            return roleClaim != null && roleClaim.Value == rol; //Verificar si el claim existe y su valor es "role"
        }
        private int? GetUserId() //Funcion para obtener el userId de las claims del usuario autenticado en el contexto de la solicitud actual.
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (IsUserInRol("Admin"))
            {
                var saleOrder = _service.Get(id);
                if (saleOrder is null)
                {
                    return NotFound($"No se encontró la venta con ID: {id}");
                }
                return Ok(saleOrder);
            }
            return Forbid();
        }

        [HttpGet("{clientId}")]
        public IActionResult GetAllByClient([FromRoute] int clientId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == clientId))
            {
                var saleOrders = _service.GetAllByClient(clientId);
                if (saleOrders.Count == 0)
                {
                    return NotFound($"No se encontró al cliente con ID: {clientId}");

                }
                return Ok(saleOrders);
            }
            return Forbid();

        }


    


        [HttpPost]
        public IActionResult AddSaleOrder([FromBody] SaleOrderCreateDTO saleOrderCreate)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == saleOrderCreate.ClientId))
            {
                _service.Add(saleOrderCreate);
                return Ok("Creada la venta para el cliente");
            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            var saleOrder = _service.Get(id);
            if (saleOrder is null)
                return NotFound($"No se encontró la venta con ID: {id}");

            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == saleOrder.Client.Id))
            {
                _service.Delete(id);
                return Ok($"Venta con ID: {id} eliminada");
            }
            return Forbid();
           
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder([FromRoute] int id, [FromBody] SaleOrderUpdateDTO saleOrderUpdate)
        {
            if (IsUserInRol("Admin"))
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
            return Forbid();
        }
    }
}

