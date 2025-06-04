using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;
using static VentaDeAccesoriosAPI.Services.DetalleVentaService;

namespace VentaDeAccesoriosAPI.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {

        private IDetalleVentaService detalleService;
        public DetalleVentaController(IDetalleVentaService detalleService)

        {
            this.detalleService = detalleService;
        }


        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] DetalleVenta detalle)
        {
            bool respuesta = await detalleService.Insert(detalle);
            if (respuesta)
            {
                return Ok("Detalle de Venta insertada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el detalle de la venta. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DetalleVenta detalle)
        {
            bool respuesta = await detalleService.Update(id, detalle);
            if (respuesta)
            {
                return Ok("Detalle de venta actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizadoel detalle de la venta. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await detalleService.Delete(id);
            if (respuesta)
            {
                return Ok("Detalle de venta eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Detalle de venta. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            DetalleVenta? detalle = await detalleService.GetById(id);
            if (detalle != null)
            {
                return Ok(detalle);
            }
            else
            {
                return NotFound(" Detalle de venta no encontrada");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<DetalleVenta> detalles = await detalleService.GetAll();
            if (detalles.Count > 0)
            {
                return Ok(detalles);
            }
            else
            {
                return NotFound("No se encontraron Detalles de ventas");
            }
        }
    }
}
