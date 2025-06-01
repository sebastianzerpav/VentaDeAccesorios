using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class RolesService : IRolesService
    {
        private readonly AppDbContext context;

        public RolesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Role role)
        {
            try
            {
                context.Roles.Add(role);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int idRol, Role role)
        {
            try
            {
                Role? foundRole = await context.Roles.FindAsync(idRol);
                if (foundRole == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundRole).CurrentValues.SetValues(role);
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

        public async Task<bool> Delete(int idRol)
        {
            try
            {
                Role? foundRole = await context.Roles.FindAsync(idRol);
                if (foundRole == null)
                {
                    return false;
                }
                else
                {
                    context.Roles.Remove(foundRole);
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

        public async Task<Role?> GetById(int idRol)
        {
            try
            {
                Role? foundRole = await context.Roles.FindAsync(idRol);
                return foundRole;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Role>> GetAll()
        {
            try
            {
                List<Role> roles = await context.Roles.ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Role>();
            }
        }
    }
    public interface IRolesService
    {
        Task<bool> Insert(Role role);
        Task<bool> Update(int idRol, Role role);
        Task<bool> Delete(int idRol);
        Task<List<Role>> GetAll();
        Task<Role?> GetById(int idRol);
    }
}
