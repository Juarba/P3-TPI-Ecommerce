using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrderDetailController : ControllerBase
    {
        private readonly ISaleOrderDetailService _saleOrderDetailService;

        public SaleOrderDetailController(ISaleOrderDetailService saleOrderDetailService)
        {
            _saleOrderDetailService = saleOrderDetailService;
        }

        [HttpGet("saleOrder/{saleOrderId}")]
        public ActionResult<List<SaleOrderDetail>> GetAllBySaleOrder(int saleOrderId)
        {
            var details = _saleOrderDetailService.GetAllBySaleOrder(saleOrderId);
            if (details == null || details.Count == 0)
            {
                return NotFound("No se encontraron detalles para la orden de venta especificada.");
            }
            return Ok(details);
        }

        [HttpGet("product/{productId}")]
        public ActionResult<List<SaleOrderDetail>> GetAllByProduct(int productId)
        {
            var details = _saleOrderDetailService.GetAllByProducts(productId);
            if (details == null || details.Count == 0)
            {
                return NotFound("No se encontraron detalles para el producto especificado.");
            }
            return Ok(details);
        }

        [HttpGet("{id}")]
        public ActionResult<SaleOrderDetail> Get(int id)
        {
            var detail = _saleOrderDetailService.Get(id);
            if (detail == null)
            {
                return NotFound("No se encontró el detalle de la orden de venta.");
            }
            return Ok(detail);
        }

        [HttpPost]
        public ActionResult Add([FromBody] SaleOrderDetailCreateDTO dto)
        {
            _saleOrderDetailService.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.SaleOrderId }, dto);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] SaleOrderDetailUpdateDTO dto)
        {
            try
            {
                _saleOrderDetailService.Update(id, dto);
                return NoContent();
            }
            catch (NotAllowedException ex)
            {
                return BadRequest(ex.Message);
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
