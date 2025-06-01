using VentaDeAccesoriosAPI.Data.Models;
namespace VentaDeAccesoriosAPI.Data.Models
{
    public class libLogin
    {
        public class AuthResponse
        {
            public string? Token { get; set; }
            public bool Resultado { get; set; }
            public string? Mensaje { get; set; }
        }

        public class AuthRequest
        {
            public string? Correo { get; set; }

            public string? Contraseña { get; set; }
        }

    }
}



