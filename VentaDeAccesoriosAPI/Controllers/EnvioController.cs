using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers

{


    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : Controller
    {


        private IEnviosService envioService;
        public EnvioController(IEnviosService envioService)

        {
            this.envioService = envioService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Envio envio)
        {
            bool respuesta = await envioService.Insert(envio);
            if (respuesta)
            {
                return Ok("Registro de envio insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el registro de envio. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Envio envio)
        {
            bool respuesta = await envioService.Update(id, envio);
            if (respuesta)
            {
                return Ok("Registro de envio actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el Registro de envio. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await envioService.Delete(id);
            if (respuesta)
            {
                return Ok("Registro de envio eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Registro de envio. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Envio? envio = await envioService.GetById(id);
            if (envio != null)
            {
                return Ok(envio);
            }
            else
            {
                return NotFound("Registro de envio no encontrada");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Envio> envios = await envioService.GetAll();
            if (envios.Count > 0)
            {
                return Ok(envios);
            }
            else
            {
                return NotFound("No se encontraron Registros de envio");
            }
        }
    }
}