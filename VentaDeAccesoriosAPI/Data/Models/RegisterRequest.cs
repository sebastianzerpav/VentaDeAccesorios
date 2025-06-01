namespace VentaDeAccesoriosAPI.Data.Models
{
    public class RegisterRequest
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
