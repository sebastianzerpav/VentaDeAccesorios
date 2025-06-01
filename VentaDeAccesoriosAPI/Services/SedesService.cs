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
                // Buscar la sede existente usando el id, no el objeto
                Sede? foundSede = await context.Sedes.FindAsync(id_sede);

                if (foundSede == null)
                {
                    return false;
                }

                // Actualizar los valores del objeto encontrado con los del objeto recibido
                context.Entry(foundSede).CurrentValues.SetValues(sede);

                // Guardar cambios en la base de datos
                await context.SaveChangesAsync();

                return true;
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
        public async Task<List<Sede>> Search(string? ciudad, string? barrio, string? pais)
        {
            try
            {
                // Empieza la consulta base
                IQueryable<Sede> query = context.Sedes.AsQueryable();

                // Filtra solo si se pasa algún valor no nulo ni vacío
                if (!string.IsNullOrWhiteSpace(ciudad))
                    query = query.Where(s => s.Ciudad.Contains(ciudad));

                if (!string.IsNullOrWhiteSpace(barrio))
                    query = query.Where(s => s.Barrio.Contains(barrio));

                if (!string.IsNullOrWhiteSpace(pais))
                    query = query.Where(s => s.Pais.Contains(pais));

                return await query.ToListAsync();
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
        Task<List<Sede>> Search(string? ciudad, string? barrio, string? pais);


    }

}
