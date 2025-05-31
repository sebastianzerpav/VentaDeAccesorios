using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace VentaDeAccesoriosAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioService usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Usuario usuario)
        {
            bool respuesta = await usuarioService.Insert(usuario);
            if (respuesta)
            {
                return Ok("Usuario insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el Usuario. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Usuario usuario)
        {
            bool respuesta = await usuarioService.Update(id, usuario);
            if (respuesta)
            {
                return Ok("Usuario actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el Usuario. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await usuarioService.Delete(id);
            if (respuesta)
            {
                return Ok("Usuario eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el usuario. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Usuario? usuario = await usuarioService.GetById(id);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound("Usuario no encontrado");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Usuario> usuarios = await usuarioService.GetAll();
            if (usuarios.Count > 0)
            {
                return Ok(usuarios);
            }
            else
            {
                return NotFound("No se encontraron usuarios");
            }
        }
    }
}
