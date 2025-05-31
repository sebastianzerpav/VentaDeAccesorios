using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SedesController : ControllerBase
    {
        private ISedesService sedesService;
        public SedesController(ISedesService sedesService)
        {
            this.sedesService = sedesService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Sede sede)
        {
            bool respuesta = await sedesService.Insert(sede);
            if (respuesta)
            {
                return Ok("la Sede se ha insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse la Sede. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Sede sede)
        {
            bool respuesta = await sedesService.Update(id, sede);
            if (respuesta)
            {
                return Ok("Sede actualizada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizada la sede. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await sedesService.Delete(id);
            if (respuesta)
            {
                return Ok("Sede eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminada la sede. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Sede? sede = await sedesService.GetById(id);
            if (sede != null)
            {
                return Ok(sede);
            }
            else
            {
                return NotFound("Sede no encontrada");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Sede> sede = await sedesService.GetAll();
            if (sede.Count > 0)
            {
                return Ok(sede);
            }
            else
            {
                return NotFound("No se encontraron Sedes");
            }
        }
    }
}
