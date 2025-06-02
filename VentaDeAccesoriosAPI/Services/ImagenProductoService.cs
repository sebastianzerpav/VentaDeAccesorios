using Microsoft.AspNetCore.Http;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class ImagenProductoService : IImagenProductoService
    {
        private readonly AppDbContext _context;

        public ImagenProductoService(AppDbContext context)
        {
            _context = context;
        }

        public Stream? Download(string nombreArchivo)
        {
            var imagen = _context.ImagenProducto.FirstOrDefault(i => i.NombreArchivo == nombreArchivo);
            if (imagen == null || imagen.Imagen == null) return null;

            return new MemoryStream(imagen.Imagen);
        }

        public bool Upload(int idProducto, IFormFile fichero)
        {
            try
            {
                using var ms = new MemoryStream();
                fichero.CopyTo(ms);
                var imagenBytes = ms.ToArray();

                var imagen = new ImagenProducto
                {
                    IdProducto = idProducto,
                    Imagen = imagenBytes,
                    NombreArchivo = fichero.FileName,
                    TipoContenido = fichero.ContentType
                };

                _context.ImagenProducto.Add(imagen);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al subir imagen: " + ex.Message);
                return false;
            }
        }

        public bool Update(int idProducto, IFormFile fichero)
        {
            var imagen = _context.ImagenProducto.FirstOrDefault(i => i.IdProducto == idProducto);
            if (imagen == null) return false;

            using var ms = new MemoryStream();
            fichero.CopyTo(ms);
            var imagenBytes = ms.ToArray();

            imagen.Imagen = imagenBytes;
            imagen.NombreArchivo = fichero.FileName;
            imagen.TipoContenido = fichero.ContentType;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int idProducto)
        {
            var imagen = _context.ImagenProducto.FirstOrDefault(i => i.IdProducto == idProducto);
            if (imagen == null) return false;

            _context.ImagenProducto.Remove(imagen);
            _context.SaveChanges();
            return true;
        }
    }

    public interface IImagenProductoService
    {
        bool Upload(int idProducto, IFormFile fichero);
        Stream? Download(string nombreArchivo);
        bool Update(int idProducto, IFormFile fichero);
        bool Delete(int idProducto);
    }
}
