using System.ComponentModel.DataAnnotations;

namespace VentaDeAccesoriosIU.Models
{
    public class libLogin
    {
        public class AuthRequest
        {
            [Required(ErrorMessage = "El correo es obligatorio.")]
            [EmailAddress(ErrorMessage = "El correo no es válido.")]
            public string Correo { get; set; } = string.Empty;
            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            public string Contraseña { get; set; } = string.Empty;
        }

        public class AuthResponse
        {
            public string Token { get; set; } = string.Empty;
            public bool Resultado { get; set; }
            public string Mensaje { get; set; } = string.Empty;
        }
    }
}
