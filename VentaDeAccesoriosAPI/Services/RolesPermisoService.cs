using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using System.Threading.Tasks;

namespace VentaDeAccesoriosAPI.Services
{
    public class RolesPermisosService : IRolesPermisosService
    {
        private readonly AppDbContext _context;

        public RolesPermisosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AsignarPermisoARol(int idRol, int idPermiso)
        {
            // Validación para evitar duplicados incluso si las propiedades son nullable
            var existe = await _context.RolesPermisos
                .AnyAsync(rp =>
                    rp.IdRol.HasValue && rp.IdPermiso.HasValue &&
                    rp.IdRol == idRol && rp.IdPermiso == idPermiso);

            if (!existe)
            {
                var nuevoRegistro = new RolesPermiso
                {
                    IdRol = idRol,
                    IdPermiso = idPermiso
                };

                _context.RolesPermisos.Add(nuevoRegistro);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }

    public interface IRolesPermisosService
    {
        Task<bool> AsignarPermisoARol(int idRol, int idPermiso);
    }
}
