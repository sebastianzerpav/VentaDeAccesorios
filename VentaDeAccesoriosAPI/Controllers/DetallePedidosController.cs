using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidosController : ControllerBase
    {
        private readonly IDetallePedidoService _detallePedidoService;
        public DetallePedidosController(IDetallePedidoService detallePedidoService)
        {
            _detallePedidoService = detallePedidoService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] DetallePedidosProveedores detalle)
        {

            bool respuesta = await _detallePedidoService.Insert(detalle);
            if (respuesta)
            {
                return Ok("Detalle de pedido insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el detalle de pedido");
            }

        }

        [HttpPut("Update/{idDetalle}")]
        public async Task<IActionResult> Update([FromRoute] int idDetalle, [FromBody] DetallePedidosProveedores detalle)
        {

            bool respuesta = await _detallePedidoService.Update(idDetalle, detalle);
            if (respuesta)
            {
                return Ok("Detalle de pedido actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el detalle de pedido.");
            }
        }

        [HttpDelete("Delete/{idDetalle}")]
        public async Task<IActionResult> Delete(int idDetalle)
        {
            bool respuesta = await _detallePedidoService.Delete(idDetalle);
            if (respuesta)
            {
                return Ok("Detalle de pedido eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el detalle de pedido");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DetallePedidosProveedores>>> GetAll()
        {
            var detalles = await _detallePedidoService.GetAll();
            return Ok(detalles);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<DetallePedidosProveedores>> GetById(int id)
        {
            DetallePedidosProveedores? detalle = await _detallePedidoService.GetById(id);
            if (detalle == null)
                return NotFound($"No se encontró el detalle de pedido con id {id}");
            return Ok(detalle);
        }
    }
}
