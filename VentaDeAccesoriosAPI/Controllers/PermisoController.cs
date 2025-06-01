using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisosService permisosService;

        public PermisoController(IPermisosService permisosService)
        {
            this.permisosService = permisosService;
        }

        // GET: api/Permisos
        [HttpGet]
        public async Task<ActionResult<List<Permiso>>> GetAll()
        {
            var permisos = await permisosService.GetAll();
            return Ok(permisos);
        }

        // GET: api/Permisos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permiso>> GetById(int id)
        {
            var permiso = await permisosService.GetById(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return Ok(permiso);
        }

        // POST: api/Permisos
        [HttpPost]
        public async Task<ActionResult> Insert(Permiso permiso)
        {
            bool result = await permisosService.Insert(permiso);
            if (result)
            {
                return CreatedAtAction(nameof(GetById), new { id = permiso.IdPermiso }, permiso);
            }
            return BadRequest("No se pudo insertar el permiso.");
        }

        // PUT: api/Permisos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Permiso permiso)
        {
            if (id != permiso.IdPermiso)
            {
                return BadRequest("El ID del permiso no coincide.");
            }

            bool result = await permisosService.Update(id, permiso);
            if (result)
            {
                return NoContent();
            }
            return NotFound("Permiso no encontrado.");
        }

        // DELETE: api/Permisos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await permisosService.Delete(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound("Permiso no encontrado.");
        }
    }
}
