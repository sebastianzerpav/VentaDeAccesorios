using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;


namespace VentaDeAccesoriosAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        private string GenerateToken(int idUsuario)
        {
            string key = configuration.GetValue<string>("JwtConfiguration:Key")!;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString(), ClaimValueTypes.Integer32));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);
            string generatedJwt = tokenHandler.WriteToken(token);

            return generatedJwt;
        }
        public async Task<libLogin.AuthResponse?> GetToken(libLogin.AuthRequest authRequest)

        {
            Usuario? foundedUser = context.Usuarios.FirstOrDefault(u => u.CorreoElectronico== authRequest.Correo && u.ContrasenaHash== authRequest.Contraseña);
            if (foundedUser == null) 
            {
                return new libLogin.AuthResponse
                {
                    Token = string.Empty,
                    Resultado = false,
                    Mensaje = "Credenciales inválidas."
                };
            }
            else
            {
                string generatedJwt = GenerateToken(foundedUser.IdUsuario);
                libLogin.AuthResponse response = new libLogin.AuthResponse
                {
                    Token = generatedJwt,
                    Resultado = true,
                    Mensaje = "Credenciales válidas. Autenticado."
                };
                return response;
            }
        }


    }

    public interface IAuthService
    {
        Task<libLogin.AuthResponse> GetToken(libLogin.AuthRequest authRequest);
    }
}
