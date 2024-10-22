using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{saleOrderId}")]
        public IActionResult GetAllBySaleOrder(int saleOrderId)
        {
            var saleOrder = _saleOrderService.Get(saleOrderId);
            if(saleOrder is null)
            {
                return NotFound($"No se encontro ninguna venta con el ID: {saleOrderId}");
            }

            var saleOrderDetails = _saleOrderDetailService.GetAllBySaleOrder(saleOrderId);
            return Ok(saleOrderDetails);

        }

        [HttpGet("{productId}")]
        public IActionResult GetAllByProduct(int productId)
        {
            var product = _productService.Get(productId);
            if(product is null)
            {
                return NotFound($"No se encontro el producto con el ID: {productId}");
            }

            var saleOrderDetails = _saleOrderDetailService.GetAllByProducts(productId);
            return Ok(saleOrderDetails);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var saleOrderDetail = _saleOrderDetailService.Get(id);
            if(saleOrderDetail is null)
            {
                return NotFound($"No se encontro la linea de venta con el ID: {id}");
            }
            return Ok(saleOrderDetail);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SaleOrderDetailCreateDTO dto)
        {
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
            
            _saleOrderDetailService.Add(dto);
            
            
            return Ok("La linea de venta fue agregada");        
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] SaleOrderDetailUpdateDTO dto)
        {
            try
            {
                _saleOrderDetailService.Update(id, dto);
                return NoContent();
            }
            catch (NotAllowedException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500,"Ocurrio un error inesperado");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _saleOrderDetailService.Delete(id);
            return NoContent();
        }
    }
}
