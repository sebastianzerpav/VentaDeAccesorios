namespace VentaDeAccesoriosAPI.Data.Models
{
    public class RegisterRequest
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private string _rol;

        public string Rol
        {
            get => string.IsNullOrWhiteSpace(_rol) || _rol.Trim().ToLower() == "null"
                ? "Cliente"
                : _rol;
            set => _rol = value;
        }
    }
}
