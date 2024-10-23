using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]/[action]")]
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

        private int? GetUserId() //Funcion para obtener el userId de las claims del usuario autenticado en el contexto de la solicitud actual.
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            if (IsUserInRol("Admin"))
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
                if (client is null)
                {
                   return NotFound($"No se encontró el cliente con el ID: {id}");
                }

                return Ok(client);
            }

            return Forbid();
        }

        [HttpGet("{name}")]
        [Authorize]
        public IActionResult GetByName(string name)
        {
            if(IsUserInRol("Admin"))
            {
                var client = _service.GetByName(name);
                if(client is null)
                {
                   return NotFound($"No se encontró el cliente con el nombre: {name}");
                }
                return Ok(client);
            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] ClientCreateDto client)
        {
            _service.Add(client);
            return Ok("El Client fue agregado exitósamente");
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateClient([FromRoute] int id, [FromBody] ClientUpdateDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            
            var userUpdate = _service.Get(id);
            if (userUpdate is null)
            {
               return NotFound($"No se encontró el cliente con el ID: {id}");
            }
            
            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == id))
            {
                _service.Update(id, dto);
               return Ok($"El cliente con ID: {id} fue actualizado correctamente");
            }
            return Forbid();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteClient(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var client = _service.Get(id);
            if (client == null)
            {
                return NotFound($"No se encontró el cliente con el ID: {id}");
            }
            
            if (IsUserInRol("Admin") || (IsUserInRol("Client") && userId == id))
            {
                _service.Delete(id);
                return Ok($"El cliente con ID: {id} fue eliminado");
            }
            return Forbid();
        }

    }
}
