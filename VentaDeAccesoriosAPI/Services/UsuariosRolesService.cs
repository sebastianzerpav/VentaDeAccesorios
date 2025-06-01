using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class UsuariosRolesService : IUsuariosRolesService
    {
        private readonly AppDbContext _context;

        public UsuariosRolesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AsignarRolAUsuario(int idUsuario, int idRol)
        {
            var existe = await _context.UsuariosRoles
                .AnyAsync(ur => ur.IdUsuario == idUsuario && ur.IdRol == idRol);

            if (!existe)
            {
                _context.UsuariosRoles.Add(new UsuariosRoles
                {
                    IdUsuario = idUsuario,
                    IdRol = idRol
                });
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<string>> ObtenerRolesDeUsuario(int idUsuario)
        {
            return await _context.UsuariosRoles
                .Where(ur => ur.IdUsuario == idUsuario && ur.IdRolNavigation != null)
                .Select(ur => ur.IdRolNavigation!.NombreRol!)
                .ToListAsync();
        }
    }
    public interface IUsuariosRolesService
    {
        Task<bool> AsignarRolAUsuario(int idUsuario, int idRol);
        Task<List<string>> ObtenerRolesDeUsuario(int idUsuario);
    }

}
