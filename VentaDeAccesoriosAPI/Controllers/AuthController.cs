using Microsoft.AspNetCore.Mvc;
using VentaDeAccesoriosAPI.Services;
using VentaDeAccesoriosAPI.Data.Models;


namespace VentaDeAccesoriosAPI.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService authService;

            public AuthController(IAuthService authService)
            {
                this.authService = authService;
            }

            [HttpPost("Auth")]
            public async Task<IActionResult> Auth([FromBody] libLogin.AuthRequest authRequest)
            {
                libLogin.AuthResponse resultado = await authService.GetToken(authRequest);
                if (resultado == null) { return Unauthorized("Credenciales inválidas."); }
                else
                {
                    return Ok(resultado);
                }
            }
        }
}
