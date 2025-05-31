using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class SedesService : ISedesService
    {
        public readonly AppDbContext context;
        public SedesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Sede sede)
        {
            try
            {
                context.Sedes.Add(sede);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_sede, Sede sede)
        {
            try
            {
                Sede? foundSede = await context.Sedes.FindAsync(sede);
                if (foundSede == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundSede).CurrentValues.SetValues(sede);
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

        public async Task<bool> Delete(int id_sede)
        {
            try
            {
                Sede? foundsede = await context.Sedes.FindAsync(id_sede);
                if (foundsede == null) { return false; }
                else
                {
                    context.Sedes.Remove(foundsede);
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

        public async Task<Sede?> GetById(int id_sede)
        {
            try
            {
                Sede? foundSede = await context.Sedes.FindAsync(id_sede);
                return foundSede;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Sede>> GetAll()
        {
            try
            {
                List<Sede> sede = await context.Sedes.ToListAsync();
                return sede;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Sede>();
            }
        }
    }
    public interface ISedesService
    {
        Task<bool> Insert(Sede sede);
        Task<bool> Update(int id_sede, Sede sede);
        Task<bool> Delete(int id_sede);
        Task<List<Sede>> GetAll();
        Task<Sede?> GetById(int id_sede);

    }

}
