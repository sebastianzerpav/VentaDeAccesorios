using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;


namespace VentaDeAccesoriosAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private IProveedoresService proveedoresService;
        public ProveedoresController(IProveedoresService proveedoresService)
        {
            this.proveedoresService = proveedoresService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Proveedores proveedores)
        {
            bool respuesta = await proveedoresService.Insert(proveedores);
            if (respuesta)
            {
                return Ok("Proveedores insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el Proveedor. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Proveedores proveedores)
        {
            bool respuesta = await proveedoresService.Update(id, proveedores);
            if (respuesta)
            {
                return Ok("Proveedor actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el proveedor. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await proveedoresService.Delete(id);
            if (respuesta)
            {
                return Ok("Proveedor eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Proveedor. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Proveedores? proveedores = await proveedoresService.GetById(id);
            if (proveedores != null)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("Proveedor no encontrado");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Proveedores> proveedores = await proveedoresService.GetAll();
            if (proveedores.Count > 0)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("No se encontraron proveedores");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            List<Proveedores> proveedores = await proveedoresService.GetByName(name);
            if (proveedores.Count > 0)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("No se encontraron proveedores con ese nombre");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetByCountry/{country}")]
        public async Task<IActionResult> GetByCountry([FromRoute] string country)
        {
            List<Proveedores> proveedores = await proveedoresService.GetByCountry(country);
            if (proveedores.Count > 0)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("No se encontraron proveedores de ese país");
            }
        }
        [AllowAnonymous]
        [HttpGet("GetByCity/{city}")]
        public async Task<IActionResult> GetByCity([FromRoute] string city)
        {
            List<Proveedores> proveedores = await proveedoresService.GetByCity(city);
            if (proveedores.Count > 0)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("No se encontraron proveedores en esa ciudad");
            }
        }
    }
}
