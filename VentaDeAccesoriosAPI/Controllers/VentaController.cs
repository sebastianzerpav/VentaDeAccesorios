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
    public class VentaController : Controller
    {
        private IVentaService ventaService;
        public VentaController(IVentaService ventaService)
        {
            this.ventaService = ventaService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Venta venta)
        {
            bool respuesta = await ventaService.Insert(venta);
            if (respuesta)
            {
                return Ok("Venta insertada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse la Venta. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Venta venta)
        {
            bool respuesta = await ventaService.Update(id, venta);
            if (respuesta)
            {
                return Ok("Venta actualizada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado la Venta. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await ventaService.Delete(id);
            if (respuesta)
            {
                return Ok("Venta eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Venta. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Venta? venta = await ventaService.GetById(id);
            if (venta != null)
            {
                return Ok(venta);
            }
            else
            {
                return NotFound("Venta no encontrada");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Venta> ventas = await ventaService.GetAll();
            if (ventas.Count > 0)
            {
                return Ok(ventas);
            }
            else
            {
                return NotFound("No se encontraron Ventas");
            }
        }
    }
}
