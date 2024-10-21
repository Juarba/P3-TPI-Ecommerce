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
        public IActionResult Get()
        {
            if(IsUserInRol("Admin"))
            {
                return Ok(_service.GetAll());

            }
            return Forbid();
        }

        [HttpPost]
        public IActionResult Add([FromBody] AdminCreateDto admin)
        {
            if(IsUserInRol("Admin"))
            {
                _service.Add(admin);
                return Ok("Admin added succesfully");
            }
            return Forbid();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AdminUpdateDto adminUpdate)
        {
            if(IsUserInRol("Admin"))
            {
                _service.Update(id, adminUpdate);
                return Ok("Admin modified succesfully");
            }
            return Forbid();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if(IsUserInRol("Admin"))
            {
                var existingAdmin = _service.Get(id);
                if (existingAdmin == null)
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

