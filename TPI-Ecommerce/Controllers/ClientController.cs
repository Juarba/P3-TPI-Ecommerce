using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        private bool IsUserInRol(string rol)
        {
            var claimsRol = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
            return claimsRol is not null && claimsRol.Value == rol;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            if(IsUserInRol("Admin"))
            {
                return Ok(_service.GetAll());
            }
            return Forbid();
          
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {

           if(IsUserInRol("Admin"))
            {
                var client = _service.Get(id);
                if (client == null)
                {
                    NotFound("No se encontro el cliente");
                }

                return Ok(client);
            }

            return Forbid();
        }


        [HttpPost]
        public IActionResult AddClient([FromBody] ClientCreateDto client)
        {
            _service.Add(client);
            return Ok("Client added succesfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateClient([FromRoute] int id, [FromBody] ClientUpdateDto dto)
        {
            var userUpdate = _service.Get(id);
            if(userUpdate == null)
            {
                return NotFound($"No se encontro el cliente con el ID: {id}");
            }

            if(IsUserInRol("Admin"))
            {
                _service.Update(id, dto);
                return Ok("El cliente fue actualizado correctamente");
            }
            return Forbid();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteClient(int id)
        {
           if(IsUserInRol("Admin"))
            {
                var client = _service.Get(id);
                if (client == null)
                {
                    return NotFound("No se encontro el cliente");
                }

                _service.Delete(id);
                return Ok("El cliente fue eliminado");
            }
            return Forbid();
        }

    }
}
