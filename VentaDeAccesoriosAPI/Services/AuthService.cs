using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using System;
using static VentaDeAccesoriosAPI.Data.Models.libLogin;

namespace VentaDeAccesoriosAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // LOGIN
        public async Task<AuthResponse?> GetToken(AuthRequest authRequest)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == authRequest.Correo);

            if (user == null || !VerifyPassword(authRequest.Contraseña!, user.ContrasenaHash))
            {
                return new AuthResponse
                {
                    Resultado = false,
                    Mensaje = "Credenciales inválidas"
                };
            }

            string token = GenerateToken(user.IdUsuario);

            return new AuthResponse
            {
                Token = token,
                Resultado = true,
                Mensaje = "Autenticación exitosa"
            };
        }

        // REGISTRO
        public async Task<AuthResponse> Register(RegisterRequest registerRequest)
        {
            bool exists = await _context.Usuarios
                .AnyAsync(u => u.CorreoElectronico == registerRequest.Correo);

            if (exists)
            {
                return new AuthResponse
                {
                    Resultado = false,
                    Mensaje = "El correo ya está registrado"
                };
            }

            string hashedPassword = HashPassword(registerRequest.Contraseña);

            var nuevoUsuario = new Usuario
            {
                Nombre = registerRequest.Nombre,
                Apellido = registerRequest.Apellido,
                CorreoElectronico = registerRequest.Correo,
                ContrasenaHash = hashedPassword
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            string token = GenerateToken(nuevoUsuario.IdUsuario);

            return new AuthResponse
            {
                Token = token,
                Resultado = true,
                Mensaje = "Registro exitoso"
            };
        }

        // VALIDAR TOKEN
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key = _configuration.GetValue<string>("JwtConfiguration:Key")!;
            var keyBytes = Encoding.UTF8.GetBytes(key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // MÉTODOS PRIVADOS

        private string GenerateToken(int idUsuario)
        {
            string key = _configuration.GetValue<string>("JwtConfiguration:Key")!;
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            var salt = Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:Salt"] ?? "defaultSalt1234");

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return HashPassword(inputPassword) == hashedPassword;
        }
    }
    public interface IAuthService
    {
        Task<AuthResponse?> GetToken(AuthRequest authRequest);
        Task<AuthResponse> Register(RegisterRequest registerRequest);
        bool ValidateToken(string token);
    }
}
