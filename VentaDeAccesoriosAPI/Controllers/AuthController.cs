using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Services;
using static VentaDeAccesoriosAPI.Data.Models.libLogin;

namespace VentaDeAccesoriosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest authRequest)
        {
            if (authRequest == null || string.IsNullOrEmpty(authRequest.Correo) || string.IsNullOrEmpty(authRequest.Contraseña))
            {
                return BadRequest(new AuthResponse { Resultado = false, Mensaje = "Correo y contraseña son requeridos." });
            }

            var authResponse = await _authService.GetToken(authRequest);

            if (authResponse == null || !authResponse.Resultado)
            {
                return Unauthorized(authResponse);
            }

            return Ok(authResponse);
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null
                || string.IsNullOrEmpty(registerRequest.Correo)
                || string.IsNullOrEmpty(registerRequest.Contraseña)
                || string.IsNullOrEmpty(registerRequest.Nombre)
                || string.IsNullOrEmpty(registerRequest.Apellido))
            {
                return BadRequest(new AuthResponse { Resultado = false, Mensaje = "Todos los campos son obligatorios." });
            }

            var authResponse = await _authService.Register(registerRequest);

            if (!authResponse.Resultado)
            {
                return Conflict(authResponse); // correo ya registrado
            }

            return Ok(authResponse);
        }

        // GET: api/auth/me
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null || !identity.IsAuthenticated)
            {
                return Unauthorized(new { Mensaje = "No autenticado" });
            }

            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var correo = identity.FindFirst(ClaimTypes.Name)?.Value;
            var roles = identity.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            return Ok(new
            {
                IdUsuario = userId,
                Correo = correo,
                Roles = roles
            });
        }

        // GET: api/auth/only-admin
        [Authorize(Roles = "Admin")]
        [HttpGet("only-admin")]
        public IActionResult OnlyAdmin()
        {
            return Ok(new { Mensaje = "¡Bienvenido admin, tenés acceso a esta ruta protegida!" });
        }
    }
}
