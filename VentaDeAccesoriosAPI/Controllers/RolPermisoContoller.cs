using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolPermisoController : ControllerBase
    {
        private readonly IRolesPermisosService _rolesPermisosService;

        public RolPermisoController(IRolesPermisosService rolesPermisosService)
        {
            _rolesPermisosService = rolesPermisosService;
        }

        // POST: api/RolPermiso/asignar
        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarPermisoARol([FromBody] AsignarPermisoDTO dto)
        {
            if (dto.IdRol <= 0 || dto.IdPermiso <= 0)
            {
                return BadRequest("IdRol y IdPermiso deben ser mayores que cero.");
            }

            var resultado = await _rolesPermisosService.AsignarPermisoARol(dto.IdRol, dto.IdPermiso);

            if (resultado)
                return Ok(new { mensaje = "Permiso asignado correctamente." });
            else
                return StatusCode(500, "Ocurrió un error al asignar el permiso.");
        }
    }

    // DTO (Data Transfer Object)
    public class AsignarPermisoDTO
    {
        public int IdRol { get; set; }
        public int IdPermiso { get; set; }
    }
}
