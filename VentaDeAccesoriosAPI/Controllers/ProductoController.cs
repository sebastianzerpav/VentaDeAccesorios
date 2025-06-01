using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;
using Microsoft.AspNetCore.Authorization;


namespace VentaDeAccesoriosAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _productoService;

        public ProductosController(IProductosService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Crear un nuevo producto
        /// </summary>
        /// <param name="producto">Datos del producto a crear</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validación adicional
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                return BadRequest("El nombre del producto es requerido");

            bool respuesta = await _productoService.Insert(producto);
            if (respuesta)
            {
                return CreatedAtAction(nameof(GetById), new { id = producto.IdProducto },
                    new { message = "Producto creado exitosamente", producto });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear el producto");
            }
        }

        /// <summary>
        /// Actualizar un producto existente
        /// </summary>
        /// <param name="id">ID del producto a actualizar</param>
        /// <param name="producto">Nuevos datos del producto</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest("ID inválido");

            // Verificar que el producto existe primero
            var productoExistente = await _productoService.GetById(id);
            if (productoExistente == null)
                return NotFound($"Producto con ID {id} no encontrado");

            bool respuesta = await _productoService.Update(id, producto);
            if (respuesta)
            {
                return Ok(new { message = "Producto actualizado exitosamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar el producto");
            }
        }

        /// <summary>
        /// Eliminar un producto
        /// </summary>
        /// <param name="id">ID del producto a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            // Verificar que el producto existe primero
            var productoExistente = await _productoService.GetById(id);
            if (productoExistente == null)
                return NotFound($"Producto con ID {id} no encontrado");

            bool respuesta = await _productoService.Delete(id);
            if (respuesta)
            {
                return Ok(new { message = "Producto eliminado exitosamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al eliminar el producto");
            }
        }

        /// <summary>
        /// Obtener todos los productos
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productos = await _productoService.GetAll();
                return Ok(new
                {
                    count = productos.Count,
                    data = productos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los productos");
            }
        }

        /// <summary>
        /// Obtener producto por ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Producto encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var producto = await _productoService.GetById(id);
            if (producto == null)
                return NotFound($"Producto con ID {id} no encontrado");

            return Ok(producto);
        }

        // SERVICIO - GetByNombre corregido con todas las relaciones
        

        // CONTROLLER - GetByNombre endpoint corregido
        /// <summary>
        /// Buscar productos por nombre
        /// </summary>
        /// <param name="nombre">Nombre o parte del nombre a buscar</param>
        /// <returns>Lista de productos que coinciden con la búsqueda</returns>
        [HttpGet("buscar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest("El parámetro 'nombre' es requerido");

            var productos = await _productoService.GetByNombre(nombre);
            return Ok(new
            {
                searchTerm = nombre,
                count = productos.Count,
                data = productos
            });
        }
    }
}