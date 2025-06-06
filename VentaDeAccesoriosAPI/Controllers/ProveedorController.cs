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
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedoresService proveedoresService;

        public ProveedorController(IProveedoresService proveedoresService)
        {
            this.proveedoresService = proveedoresService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Proveedores proveedores)
        {
            bool respuesta = await proveedoresService.Insert(proveedores);
            if (respuesta)
            {
                return Ok("Proveedor insertado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo insertarse el Proveedor. Revisar consola de errores.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Proveedores proveedores)
        {
            bool respuesta = await proveedoresService.Update(id, proveedores);
            if (respuesta)
            {
                return Ok("Proveedor actualizado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser actualizado el proveedor. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool respuesta = await proveedoresService.Delete(id);
            if (respuesta)
            {
                return Ok("Proveedor eliminado exitosamente");
            }
            else
            {
                return StatusCode(500, "No pudo ser eliminado el Proveedor. Revisar consola de errores o revisar si existe objeto en la base de datos.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Proveedores? proveedores = await proveedoresService.GetById(id);
            if (proveedores != null)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("Proveedor no encontrado");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Proveedores> proveedores = await proveedoresService.GetAll();
            if (proveedores.Count > 0)
            {
                return Ok(proveedores);
            }
            else
            {
                return NotFound("No se encontraron proveedores");
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string? nombre, [FromQuery] string? ciudad, [FromQuery] string? pais)
        {
            var resultados = await proveedoresService.Buscar(nombre, ciudad, pais);
            return Ok(resultados);

        }
    }
}
