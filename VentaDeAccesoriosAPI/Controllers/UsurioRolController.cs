using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuariosRolesService _usuariosRolesService;

        public UsuarioRolController(IUsuariosRolesService usuariosRolesService)
        {
            _usuariosRolesService = usuariosRolesService;
        }

        // POST: api/UsuarioRol/asignar
        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarRolAUsuario([FromBody] AsignarRolDTO dto)
        {
            if (dto.IdUsuario <= 0 || dto.IdRol <= 0)
                return BadRequest("IdUsuario e IdRol deben ser mayores que cero.");

            var resultado = await _usuariosRolesService.AsignarRolAUsuario(dto.IdUsuario, dto.IdRol);

            if (resultado)
                return Ok(new { mensaje = "Rol asignado correctamente al usuario." });
            else
                return StatusCode(500, "Error al asignar el rol.");
        }

        // GET: api/UsuarioRol/roles/5
        [HttpGet("roles/{idUsuario}")]
        public async Task<IActionResult> ObtenerRolesDeUsuario(int idUsuario)
        {
            var roles = await _usuariosRolesService.ObtenerRolesDeUsuario(idUsuario);

            if (roles == null || roles.Count == 0)
                return NotFound(new { mensaje = "El usuario no tiene roles asignados." });

            return Ok(roles);
        }
    }

    // DTO para asignar rol
    public class AsignarRolDTO
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
    }
}
