using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderService _service;

        public SaleOrderController(ISaleOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(int id) 
        {
            if (id <= 0) 
            {
                return BadRequest("Id incorrecto");
            }
            return Ok(_service.Get(id));
        }

        [HttpGet("id")]
        public IActionResult GetAll() 
        {
            return Ok(_service.GetAll());
        }

        
    }
}
