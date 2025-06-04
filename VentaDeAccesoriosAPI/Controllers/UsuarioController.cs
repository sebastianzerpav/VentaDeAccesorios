using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    //[Authorize(Roles = "Administrador,Vendedor")]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase

    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }


        // [AllowAnonymous] le quita la protección a este método específico

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Usuario usuario)
        {
            bool respuesta = await usuarioService.Insert(usuario);
            if (respuesta)
                return Ok("El usuario fue creado exitosamente.");
            else
                return StatusCode(500, "Error al insertar el usuario.");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Usuario usuario)
        {
            bool respuesta = await usuarioService.Update(id, usuario);
            if (respuesta)
                return Ok("Usuario actualizado correctamente.");
            else
                return NotFound("No se encontró el usuario a actualizar.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await usuarioService.Delete(id);
            if (respuesta)
                return Ok("Usuario eliminado correctamente.");
            else
                return NotFound("No se encontró el usuario a eliminar.");
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Usuario? usuario = await usuarioService.GetById(id);
            if (usuario != null)
                return Ok(usuario);
            else
                return NotFound("Usuario no encontrado.");
        }

        [HttpGet("GetByName/{nombre}")]
        public async Task<IActionResult> GetByName([FromRoute] string nombre)
        {
            var usuarios = await usuarioService.GetByName(nombre);
            if (usuarios.Any())
                return Ok(usuarios);
            else
                return NotFound("No se encontraron usuarios.");
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await usuarioService.GetAll();
            if (usuarios.Count > 0)
                return Ok(usuarios);
            else
                return NotFound("No hay usuarios registrados.");
        }
    }
}
