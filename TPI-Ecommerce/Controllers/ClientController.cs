using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        public ClientController(IClientService clientService) 
        {
            _service = clientService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll()); 
        }

    }
}
