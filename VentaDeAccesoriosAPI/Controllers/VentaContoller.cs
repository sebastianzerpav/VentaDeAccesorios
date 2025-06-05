using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] 
    public class VentasController : ControllerBase
    {
        private readonly IVentasService _ventasService;

        public VentasController(IVentasService ventasService)
        {
            _ventasService = ventasService;
        }

        // GET: api/ventas
        [HttpGet("GetAll")]
        
        public async Task<ActionResult<IEnumerable<Venta>>> GetAll()
        {
            var ventas = await _ventasService.GetAll();
            return Ok(ventas);
        }

        // GET: api/ventas/{id}
        [HttpGet("GetById/{id}")]
        //[AllowAnonymous] // Permite acceso sin autenticación para pruebas
        public async Task<ActionResult<Venta>> GetById(int id)
        {
            var venta = await _ventasService.GetById(id);
            if (venta == null)
                return NotFound($"No se encontró la venta con id {id}");
            return Ok(venta);
        }

        // POST: api/ventas
        [HttpPost("Insert")]
        //[Authorize] // Habilita esta línea para requerir autorización en este método
        //[AllowAnonymous] // Temporal para pruebas sin autenticación
        public async Task<ActionResult> Insert(Venta venta)
        {
            var result = await _ventasService.Insert(venta);
            if (!result)
                return BadRequest("No se pudo crear la venta. Verifica que los datos sean correctos.");

            return CreatedAtAction(nameof(GetById), new { id = venta.IdVenta }, venta);
        }

        // PUT: api/ventas/{id}
        [HttpPut("Update/{id}")]
        //[Authorize]
        [AllowAnonymous] // Temporal para pruebas
        public async Task<ActionResult> Update(int id, Venta venta)
        {
            if (id != venta.IdVenta)
                return BadRequest("El id del parámetro no coincide con el id de la venta.");

            var result = await _ventasService.Update(id, venta);
            if (!result)
                return NotFound($"No se encontró la venta con id {id}");

            return NoContent();
        }

        // DELETE: api/ventas/{id}
        [HttpDelete("Delete/{id}")]
        //[Authorize]
        [AllowAnonymous] // Temporal para pruebas
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _ventasService.Delete(id);
            if (!result)
                return NotFound($"No se encontró la venta con id {id}");

            return NoContent();
        }

        [HttpGet("GetUltimo")]
        public async Task<IActionResult> GetUltimo()
        {
            Venta venta = await _ventasService.GetUltimo();
            return Ok(venta);
        }
    }
}
