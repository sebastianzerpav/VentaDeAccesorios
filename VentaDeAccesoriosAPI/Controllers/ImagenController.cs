using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenProductoController : ControllerBase
    {
        private readonly IImagenProductoService _imagenService;

        public ImagenProductoController(IImagenProductoService imagenService)
        {
            _imagenService = imagenService;
        }

        [HttpPost("{idProducto}")]
        public IActionResult SubirImagen(int idProducto, IFormFile fichero)
        {
            if (fichero == null || fichero.Length == 0)
                return BadRequest("Archivo no válido.");

            var resultado = _imagenService.Upload(idProducto, fichero);
            if (!resultado) return StatusCode(500, "Error al subir la imagen.");

            return Ok("Imagen subida exitosamente.");
        }

        [HttpGet("{nombreArchivo}")]
        public IActionResult DescargarImagen(string nombreArchivo)
        {
            var stream = _imagenService.Download(nombreArchivo);
            if (stream == null) return NotFound("Imagen no encontrada.");

            // Podés obtener el tipo MIME desde el servicio si se desea
            return File(stream, "application/octet-stream", nombreArchivo);
        }

        [HttpPut("{idProducto}")]
        public IActionResult ActualizarImagen(int idProducto, IFormFile fichero)
        {
            if (fichero == null || fichero.Length == 0)
                return BadRequest("Archivo no válido.");

            var resultado = _imagenService.Update(idProducto, fichero);
            if (!resultado) return NotFound("Imagen no encontrada o error al actualizar.");

            return Ok("Imagen actualizada exitosamente.");
        }

        [HttpDelete("{idProducto}")]
        public IActionResult EliminarImagen(int idProducto)
        {
            var resultado = _imagenService.Delete(idProducto);
            if (!resultado) return NotFound("Imagen no encontrada.");

            return Ok("Imagen eliminada exitosamente.");
        }
    }
}
