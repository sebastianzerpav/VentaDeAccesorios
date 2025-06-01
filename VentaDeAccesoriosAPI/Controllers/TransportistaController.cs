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
    public class TransportistaController : Controller
    {
        private ITransportistasService transportistasService;
        public TransportistaController(ITransportistasService transportistasService)
        {
            this.transportistasService = transportistasService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Transportista transportista)
        {
            bool respuesta = await transportistasService.Insert(transportista);
            if (respuesta)
            {
                return Ok("Transportista insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el transportista. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Transportista transportista)
        {
            bool respuesta = await transportistasService.Update(id, transportista);
            if (respuesta)
            {
                return Ok("Transportista actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el transportita. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await transportistasService.Delete(id);
            if (respuesta)
            {
                return Ok("Trasportista eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Transportista. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Transportista? transportista = await transportistasService.GetById(id);
            if (transportista != null)
            {
                return Ok(transportista);
            }
            else
            {
                return NotFound("Transportista no encontrado");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Transportista> transportistas = await transportistasService.GetAll();
            if (transportistas.Count > 0)
            {
                return Ok(transportistas);
            }
            else
            {
                return NotFound("No se encontraron Transportistas");
            }
        }
    }
}