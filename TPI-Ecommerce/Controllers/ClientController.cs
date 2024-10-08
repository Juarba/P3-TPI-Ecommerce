using Application.Interfaces;
using Application.Models;
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
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _service.Get(id);
            if (client == null)
            {
                NotFound("No se encontro el cliente");
            }

            return Ok(client);
        }


        [HttpPost]
        public IActionResult AddClient([FromBody] ClientCreateDto client)
        {
            _service.Add(client);
            return Ok("Client added succesfully");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = _service.Get(id);
            if (client == null)
            {
                return NotFound("No se encontro el cliente");
            }

            _service.Delete(id);
            return Ok("El cliente fue eliminado");
        }

    }
}
