﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Controllers
{
    //[Authorize]  // Todas las acciones requieren autenticación
    [Route("api/[controller]")]
    [ApiController]
    public class SedeController : ControllerBase
    {
        private readonly ISedesService sedesService;

        public SedeController(ISedesService sedesService)
        {
            this.sedesService = sedesService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Sede sede)
        {
            bool respuesta = await sedesService.Insert(sede);
            if (respuesta)
            {
                return Ok("La sede se ha insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse la sede. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Sede sede)
        {
            bool respuesta = await sedesService.Update(id, sede);
            if (respuesta)
            {
                return Ok("Sede actualizada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizada la sede. Revisar consola de errores o revisar si existe el objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await sedesService.Delete(id);
            if (respuesta)
            {
                return Ok("Sede eliminada exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminada la sede. Revisar consola de errores o verificar si existe el objeto en la base de datos.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Sede? sede = await sedesService.GetById(id);
            if (sede != null)
            {
                return Ok(sede);
            }
            else
            {
                return NotFound("Sede no encontrada");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Sede> sedes = await sedesService.GetAll();
            if (sedes.Count > 0)
            {
                return Ok(sedes);
            }
            else
            {
                return NotFound("No se encontraron sedes");
            }
        }
        // GET: api/sedes/search?ciudad=Medellin&barrio=Centro&pais=Colombia
        [HttpGet("search")]
        public async Task<ActionResult<List<Sede>>> Search([FromQuery] string? ciudad, [FromQuery] string? barrio, [FromQuery] string? pais)
        {
            var results = await sedesService.Search(ciudad, barrio, pais);
            return results;
        }
    }
}