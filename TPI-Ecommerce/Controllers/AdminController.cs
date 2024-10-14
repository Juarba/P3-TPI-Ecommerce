using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody] AdminCreateDto admin)
        {
            _service.Add(admin);
            return Ok("Admin added succesfully");

        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AdminUpdateDto adminUpdate)
        {
            _service.Update(id, adminUpdate);
            return Ok("Admin modified succesfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var existingAdmin = _service.Get(id);
            if (existingAdmin == null)
            {
                return NotFound($"No se encontró ningún Admin con el ID: {id}");
            }
            _service.Delete(id);
            return Ok($"Admin con ID: {id} eliminado");
        }
    }
}

