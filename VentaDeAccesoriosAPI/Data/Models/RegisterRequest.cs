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
                : Capitalizar(_rol.Trim());
            set => _rol = value;
        }

        private string Capitalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
        }
    }
}
