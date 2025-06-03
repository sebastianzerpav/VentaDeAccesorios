using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.DTO;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosProveedorController : ControllerBase
    {
        private readonly IProductosProveedoresService _productoProveedoresService;

        public ProductosProveedorController(IProductosProveedoresService productoProveedoresService)
        {
            _productoProveedoresService = productoProveedoresService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ProductosProveedoresNaN> relaciones = await _productoProveedoresService.GetAll();
            if (!relaciones.Any()) { return NotFound("No hay ninguna relación"); }
            else
            {
                return Ok(relaciones);
            }
        }

        [HttpGet("GetById/{id_relacion}")]
        public async Task<IActionResult> GetById([FromRoute] int id_relacion)
        {
            ProductosProveedoresNaN? relacion = await _productoProveedoresService.GetById(id_relacion);
            if (relacion == null) { return NotFound("No hay ninguna relación con ese id"); }
            else
            {
                return Ok(relacion);
            }
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ProductosProveedores relacion)
        {

            bool respuesta = await _productoProveedoresService.Insert(relacion);
            if (respuesta)
            {
                return Ok("Relación insertada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse la relación");
            }

        }

        [HttpPut("Update/{id_relacion}")]
        public async Task<IActionResult> Update([FromRoute]int id_relacion, [FromBody] ProductosProveedores relacion)
        {

            bool respuesta = await _productoProveedoresService.Update(id_relacion, relacion);
            if (respuesta)
            {
                return Ok("Relación actualizada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizada la relación.");
            }
        }

        [HttpDelete("Delete/{id_relacion}")]
        public async Task<IActionResult> Delete(int id_relacion)
        {
            bool respuesta = await _productoProveedoresService.Delete(id_relacion);
            if (respuesta)
            {
                return Ok("Relación eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminada la relación");
            }
        }
    }
}
