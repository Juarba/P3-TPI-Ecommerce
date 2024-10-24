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
        private readonly ISaleOrderService _saleOrderService;
        private readonly IClientService _clientService;

        public SaleOrderController(ISaleOrderService saleOrderService, IClientService clientService)
        {
            _saleOrderService = saleOrderService;
            _clientService = clientService;
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
                var saleOrder = _saleOrderService.Get(id);
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

            var client = _clientService.Get(clientId);
            if (client is null)
            {
                return NotFound($"No se encontró al cliente con ID: {clientId}");
            }

            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == clientId))
            {
                var saleOrders = _saleOrderService.GetAllByClient(clientId);
                
                if (saleOrders.Count == 0)
                {
                    return BadRequest($"El cliente con ID: {clientId} todavía no hizo ninguna compra");

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

            var client = _clientService.Get(saleOrderCreate.ClientId);
            if (client is null)
            {
                return NotFound($"No se encontró al cliente con ID: {saleOrderCreate.ClientId}");
            }

            if(saleOrderCreate.PaymentMethod.ToLower() != "efectivo" && saleOrderCreate.PaymentMethod.ToLower() != "tarjeta")
            {
                return BadRequest("La compra debe ser sólo en efectivo o tarjeta");
            }

            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == saleOrderCreate.ClientId))
            {
                _saleOrderService.Add(saleOrderCreate);
                return Ok($"Creada la venta para el cliente ID: {saleOrderCreate.ClientId}");
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
            var saleOrder = _saleOrderService.Get(id);
            if (saleOrder is null)
                return NotFound($"No se encontró la venta con ID: {id}");

            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == saleOrder.Client.Id))
            {
                _saleOrderService.Delete(id);
                return Ok($"Venta con ID: {id} eliminada");
            }
            return Forbid();
           
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder([FromRoute] int id, [FromBody] SaleOrderUpdateDTO saleOrderUpdate)
        {
            if (IsUserInRol("Admin"))
            {
                var saleOrder = _saleOrderService.Get(id);
                if(saleOrder == null)
                {
                    return NotFound($"No se encontró la venta con el ID: {id}");
                }

                if(saleOrderUpdate.PaymentMethod.ToLower() != "efectivo" && saleOrderUpdate.PaymentMethod.ToLower() != "tarjeta")
                {
                    return BadRequest("La compra debe ser sólo en efectivo o tarjeta");
                }

                try
                {
                    _saleOrderService.Update(id, saleOrderUpdate);
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

