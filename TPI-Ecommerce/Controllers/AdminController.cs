using Application.Interfaces;
using Application.Models;
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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        private bool IsUserInRol(string rol)
        {
            var claimsRol = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
            return claimsRol is not null && claimsRol.Value == rol;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if(IsUserInRol("Admin"))
            {
                return Ok(_service.GetAll());

            }
            return Forbid();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if(IsUserInRol("Admin"))
            {
                var admin = _service.Get(id);
                if (admin is null)
                {
                    return NotFound($"No se encontró ningún Admin con el ID: {id}");
                }

                return Ok(admin);
            }
            return Forbid();     
        }

        [HttpPost]
        public IActionResult AddAdmin([FromBody] AdminCreateDto admin)
        {
            if(IsUserInRol("Admin"))
            {
                _service.Add(admin);
                return Ok("El Admin fue creado exitósamente");
            }
            return Forbid();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AdminUpdateDto adminUpdate)
        {
            if (IsUserInRol("Admin"))
            {
                try
                {
                    _service.Update(id, adminUpdate);
                    return Ok("Admin modificado exitosamente");
                }
                catch (NotAllowedException e)
                {
                    return BadRequest("Error, Admin no Encontrado");
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Ha ocurrido un Error inesperado: " + e.Message);
                }

            }
            return Forbid();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin([FromRoute] int id)
        {
            if(IsUserInRol("Admin"))
            {
                var existingAdmin = _service.Get(id);
                if (existingAdmin is null)
                {
                    return NotFound($"No se encontró ningún Admin con el ID: {id}");
                }
                _service.Delete(id);
                return Ok($"Admin con ID: {id} eliminado");
            }
            return Forbid();
        }
    }
}

