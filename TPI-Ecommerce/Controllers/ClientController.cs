using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService) 
        {
            _clientService = clientService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientService.GetAll()); 
        }
    }
}
