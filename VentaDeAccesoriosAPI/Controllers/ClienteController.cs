using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClientesService clientesService;

        public ClienteController(IClientesService clientesService)
        {
            this.clientesService = clientesService;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            var clientes = await clientesService.GetAll();
            return Ok(clientes);
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await clientesService.GetById(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult> Insert(Cliente cliente)
        {
            var result = await clientesService.Insert(cliente);
            if (!result)
                return BadRequest("No se pudo insertar el cliente.");
            return Ok("Cliente insertado correctamente.");
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Cliente cliente)
        {
            var result = await clientesService.Update(id, cliente);
            if (!result)
                return NotFound("Cliente no encontrado o no se pudo actualizar.");

            return Ok("Cliente actualizado correctamente.");
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await clientesService.Delete(id);
            if (!result)
                return NotFound("Cliente no encontrado o no se pudo eliminar.");

            return Ok("Cliente eliminado correctamente.");
        }
    }
}
