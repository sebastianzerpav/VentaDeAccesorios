using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoProveedorController : ControllerBase
    {
        private readonly IPedidoProveedorService _pedidoProveedorService;
        public PedidoProveedorController(IPedidoProveedorService pedidoProveedorService)
        {
            _pedidoProveedorService = pedidoProveedorService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] PedidosProveedores pedido)
        {

            bool respuesta = await _pedidoProveedorService.Insert(pedido);
            if (respuesta)
            {
                return Ok("Pedido insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el pedido");
            }

        }

        [HttpPut("Update/{idPedido}")]
        public async Task<IActionResult> Update([FromRoute] int idPedido, [FromBody] PedidosProveedores pedido)
        {

            bool respuesta = await _pedidoProveedorService.Update(idPedido, pedido);
            if (respuesta)
            {
                return Ok("Pedido actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el pedido.");
            }
        }

        [HttpDelete("Delete/{idPedido}")]
        public async Task<IActionResult> Delete(int idPedido)
        {
            bool respuesta = await _pedidoProveedorService.Delete(idPedido);
            if (respuesta)
            {
                return Ok("Pedido eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el pedido");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PedidosProveedores>>> GetAll()
        {
            var pedidos = await _pedidoProveedorService.GetAll();
            return Ok(pedidos);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PedidosProveedores>> GetById(int id)
        {
            var pedido = await _pedidoProveedorService.GetById(id);
            if (pedido == null)
                return NotFound($"No se encontró el pedido con id {id}");
            return Ok(pedido);
        }

    }
}
