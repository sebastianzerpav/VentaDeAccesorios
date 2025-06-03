using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _productoService;

        public ProductosController(IProductosService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAll();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _productoService.GetById(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByNombre([FromQuery] string nombre)
        {
            var productos = await _productoService.GetByNombre(nombre);
            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                return BadRequest("El nombre del producto es requerido");

            var resultado = await _productoService.Insert(producto);
            if (resultado)
                return CreatedAtAction(nameof(GetById), new { id = producto.IdProducto }, producto);

            return StatusCode(500, "Error interno al crear el producto");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _productoService.Update(id, producto);
            if (!resultado)
                return NotFound("Producto no encontrado o error al actualizar");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _productoService.Delete(id);
            if (!resultado)
                return NotFound("Producto no encontrado error al eliminar (primero eliminar imagenes)");

            return NoContent();
        }
    }
}
