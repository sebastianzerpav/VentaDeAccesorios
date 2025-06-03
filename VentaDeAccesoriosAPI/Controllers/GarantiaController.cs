using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarantiaController : ControllerBase
    {
        private readonly IGarantiaService _service;

        public GarantiaController(IGarantiaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Garantia>>> GetGarantias()
        {
            var lista = await _service.ObtenerGarantias();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Garantia>> GetGarantia(int id)
        {
            var garantia = await _service.ObtenerGarantiaPorId(id);
            if (garantia == null) return NotFound();
            return Ok(garantia);
        }

        [HttpPost]
        public async Task<ActionResult<Garantia>> CrearGarantia(Garantia garantia)
        {
            var nuevaGarantia = await _service.CrearGarantia(garantia);
            return CreatedAtAction(nameof(GetGarantia), new { id = nuevaGarantia.IdGarantia }, nuevaGarantia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarGarantia(int id, Garantia garantia)
        {
            var actualizado = await _service.ActualizarGarantia(id, garantia);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarGarantia(int id)
        {
            var eliminado = await _service.EliminarGarantia(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
