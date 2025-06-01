using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class PermisosService : IPermisosService
    {
        private readonly AppDbContext context;

        public PermisosService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Permiso permiso)
        {
            try
            {
                context.Permisos.Add(permiso);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int idPermiso, Permiso permiso)
        {
            try
            {
                Permiso? foundPermiso = await context.Permisos.FindAsync(idPermiso);
                if (foundPermiso == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundPermiso).CurrentValues.SetValues(permiso);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Delete(int idPermiso)
        {
            try
            {
                Permiso? foundPermiso = await context.Permisos.FindAsync(idPermiso);
                if (foundPermiso == null)
                {
                    return false;
                }
                else
                {
                    context.Permisos.Remove(foundPermiso);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<Permiso?> GetById(int idPermiso)
        {
            try
            {
                Permiso? foundPermiso = await context.Permisos.FindAsync(idPermiso);
                return foundPermiso;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Permiso>> GetAll()
        {
            try
            {
                List<Permiso> permisos = await context.Permisos.ToListAsync();
                return permisos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Permiso>();
            }
        }
    }
    public interface IPermisosService
    {
        Task<bool> Insert(Permiso permiso);
        Task<bool> Update(int idPermiso, Permiso permiso);
        Task<bool> Delete(int idPermiso);
        Task<List<Permiso>> GetAll();
        Task<Permiso?> GetById(int idPermiso);
    }
}
