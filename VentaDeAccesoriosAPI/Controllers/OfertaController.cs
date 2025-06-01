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
    public class OfertaController : Controller
    {
        private IOfertaService ofertaService;
        public OfertaController(IOfertaService ofertaService)
        {
            this.ofertaService = ofertaService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Oferta oferta)
        {
            bool respuesta = await ofertaService.Insert(oferta);
            if (respuesta)
            {
                return Ok("Oferta insertada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse la Oferta. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Oferta oferta)
        {
            bool respuesta = await ofertaService.Update(id, oferta);
            if (respuesta)
            {
                return Ok("Oferta actualizada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el Oferta. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await ofertaService.Delete(id);
            if (respuesta)
            {
                return Ok("Oferta eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Oferta. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Oferta? oferta = await ofertaService.GetById(id);
            if (oferta != null)
            {
                return Ok(oferta);
            }
            else
            {
                return NotFound("Oferta no encontrada");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Oferta> oferta = await ofertaService.GetAll();
            if (oferta.Count > 0)
            {
                return Ok(oferta);
            }
            else
            {
                return NotFound("No se encontraron Ofertas");
            }
        }

    }
}
