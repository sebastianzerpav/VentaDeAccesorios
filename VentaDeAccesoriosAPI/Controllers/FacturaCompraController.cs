using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaCompraController : ControllerBase
    {
        private readonly IFacturaCompraService _facturaCompraService;
        public FacturaCompraController(IFacturaCompraService facturaCompraService)
        {
            _facturaCompraService = facturaCompraService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] FacturasCompra factura)
        {

            bool respuesta = await _facturaCompraService.Insert(factura);
            if (respuesta)
            {
                return Ok("Factura insertada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse la factura");
            }

        }

        [HttpPut("Update/{idFactura}")]
        public async Task<IActionResult> Update([FromRoute] int idFactura, [FromBody] FacturasCompra factura)
        {

            bool respuesta = await _facturaCompraService.Update(idFactura, factura);
            if (respuesta)
            {
                return Ok("Factura actualizada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado la factura.");
            }
        }

        [HttpDelete("Delete/{idFactura}")]
        public async Task<IActionResult> Delete(int idFactura)
        {
            bool respuesta = await _facturaCompraService.Delete(idFactura);
            if (respuesta)
            {
                return Ok("Factura eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado la factura");
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<FacturasCompra>>> GetAll()
        {
            var facturas = await _facturaCompraService.GetAll();
            return Ok(facturas);
        }

        [HttpGet("GetById/{idFactura}")]
        public async Task<ActionResult<FacturasCompra>> GetById(int idFactura)
        {
            var factura = await _facturaCompraService.GetById(idFactura);
            if (factura == null)
                return NotFound($"No se encontró la factura con id {idFactura}");
            return Ok(factura);
        }
    }
}
