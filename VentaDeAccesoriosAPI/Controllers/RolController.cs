using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolControl : ControllerBase
    {
        private readonly IRolesService rolesService;

        public RolControl(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAll()
        {
            var roles = await rolesService.GetAll();
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await rolesService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult> Insert(Role role)
        {
            bool result = await rolesService.Insert(role);
            if (result)
            {
                return CreatedAtAction(nameof(GetById), new { id = role.IdRol }, role);
            }
            return BadRequest("No se pudo insertar el rol.");
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Role role)
        {
            if (id != role.IdRol)
            {
                return BadRequest("El ID del rol no coincide.");
            }

            bool result = await rolesService.Update(id, role);
            if (result)
            {
                return NoContent();
            }
            return NotFound("Rol no encontrado.");
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await rolesService.Delete(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound("Rol no encontrado.");
        }
    }
}
