using Microsoft.AspNetCore.Http;

namespace VentaDeAccesoriosAPI.Data.DTOs
{
    public class ImagenProductoDto
    {
        public IFormFile? Fichero { get; set; }
    }
}
