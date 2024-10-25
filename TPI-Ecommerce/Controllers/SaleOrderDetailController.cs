using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleOrderDetailController : ControllerBase
    {
        private readonly ISaleOrderDetailService _saleOrderDetailService;
        private readonly ISaleOrderService _saleOrderService;
        private readonly IProductService _productService;


        public SaleOrderDetailController(ISaleOrderDetailService saleOrderDetailService, ISaleOrderService saleOrderService, IProductService productService)
        {
            _saleOrderDetailService = saleOrderDetailService;
            _saleOrderService = saleOrderService;
            _productService = productService;
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

        [HttpGet("{saleOrderId}")]
        public IActionResult GetAllBySaleOrder(int saleOrderId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var saleOrder = _saleOrderService.Get(saleOrderId);
            if(saleOrder is null)
            {
                return NotFound($"No se encontro ninguna venta con el ID: {saleOrderId}");
            }

            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == saleOrder.Client.Id))
            {
                var saleOrderDetails = _saleOrderDetailService.GetAllBySaleOrder(saleOrderId);
                return Ok(saleOrderDetails);
            }
            return Forbid();
                

        }

        [HttpGet("{productId}")]
        public IActionResult GetAllByProduct(int productId)
        {
            if (IsUserInRol("Admin"))
            {
                var product = _productService.Get(productId);
                if (product is null)
                {
                    return NotFound($"No se encontro el producto con el ID: {productId}");
                }

                var saleOrderDetails = _saleOrderDetailService.GetAllByProducts(productId);
                return Ok(saleOrderDetails);
            }
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var saleOrderDetail = _saleOrderDetailService.Get(id);
            if (saleOrderDetail is null)
            {
                return NotFound($"No se encontro la linea de venta con el ID: {id}");
            }

            if (IsUserInRol("Admin"))
            {
                return Ok(saleOrderDetail);
            }

            return Forbid();
        }

        [HttpPost]
        public IActionResult AddSaleOrderDetail([FromBody] SaleOrderDetailCreateDTO dto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            
            var actualSaleOrder = _saleOrderService.Get(dto.SaleOrderId);
            if(actualSaleOrder is null)
            {
                return NotFound($"No se encontro la venta con el ID {dto.SaleOrderId}");
            }

            var productSelected = _productService.Get(dto.ProductId);
            if(productSelected is null)
            {
                return NotFound($"No se encontro el producto con el ID {dto.ProductId}");
            }

            if (productSelected.Stock < dto.Amount)
                return BadRequest("Stock Insuficiente");

            if(dto.Amount <= 0)
            {
                return BadRequest("La cantidad debe ser mayor que 0");
            }

            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == actualSaleOrder.Client.Id))
            {
                _productService.Update(productSelected.Id, new ProductUpdateDto()
                {
                    Price = productSelected.Price,
                    Stock = productSelected.Stock - dto.Amount,
                });

                _saleOrderDetailService.Add(dto);
                return Ok("La linea de venta fue agregada");

            }
            return Forbid();

                
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrderDetail(int id, [FromBody] SaleOrderDetailUpdateDTO dto, int saleOrderId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            try
            {
                var productSelected = _productService.Get(dto.ProductId);
                if (productSelected is null)
                {
                    return NotFound($"No se encontró el producto con el ID {dto.ProductId}");
                }

                var actualSaleOrder = _saleOrderService.Get(saleOrderId);
                if (actualSaleOrder is null)
                {
                    return NotFound($"No se encontró la venta con ID: {saleOrderId}");
                }

                var saleOrderDetail = _saleOrderDetailService.Get(id);
                if(saleOrderDetail is null)
                {
                    return NotFound($"No se encontró la línea de venta con el ID: {id}");
                }


                if (productSelected.Stock < dto.Amount)
                    return BadRequest("Stock Insuficiente");

                if (dto.Amount <= 0)
                {
                    return BadRequest("La cantidad debe ser mayor que 0");
                }

                int previousAmount = saleOrderDetail.Amount;
                int newAmount = dto.Amount;

                if(newAmount > previousAmount)
                {
                    productSelected.Stock -= (newAmount - previousAmount);
                }
                else
                {
                    productSelected.Stock += (previousAmount - newAmount);
                }

                if (IsUserInRol("Admin") || IsUserInRol("Client") && userId == actualSaleOrder.Client.Id)
                {
                    _productService.Update(productSelected.Id, new ProductUpdateDto()
                    {
                        Price = productSelected.Price,
                        Stock = productSelected.Stock
                    });

                    _saleOrderDetailService.Update(id, dto, saleOrderId);
                    return Ok("La línea de venta fue modificada");
                }
                return Forbid();
            }
            catch(NotFoundException ex)
            {
                return (NotFound(ex.Message));
            }

            catch(Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrderDetail(int id, int saleOrderId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var actualSaleOrder = _saleOrderService.Get(saleOrderId);
            if(actualSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {saleOrderId}");
            }

            if (IsUserInRol("Admin") || IsUserInRol("Client") && userId == actualSaleOrder.Client.Id)
            {
                var actualSaleOrderDetail = _saleOrderDetailService.Get(id);
                if (actualSaleOrderDetail is null)
                {
                return NotFound($"No se encontró la la linea de venta con el ID: {id}");

                }
                _saleOrderDetailService.Delete(id);
                return Ok($"La linea de venta con ID {id} fue eliminada");
            }
            return Forbid();
           
        }
    }
}
