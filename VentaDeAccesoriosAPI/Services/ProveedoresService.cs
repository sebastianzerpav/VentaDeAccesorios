using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class ProveedoresService : IProveedoresService
    {
        public readonly AppDbContext context;

        public ProveedoresService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Proveedores proveedores)
        {
            try
            {
                context.Proveedores.Add(proveedores);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_proveedor, Proveedores proveedores)
        {
            try
            {
                Proveedores? foundProveedores = await context.Proveedores.FindAsync(id_proveedor);
                if (foundProveedores == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundProveedores).CurrentValues.SetValues(proveedores);
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

        public async Task<bool> Delete(int id_proveedor)
        {
            try
            {
                var foundProveedor = await context.Proveedores.FindAsync(id_proveedor);
                if (foundProveedor == null)
                    return false;

                var entry = context.Entry(foundProveedor);
                foreach (var collection in entry.Collections)
                {
                    await collection.LoadAsync();
                    (collection.CurrentValue as System.Collections.IList)?.Clear();
                }

                context.Proveedores.Remove(foundProveedor);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<Proveedores?> GetById(int id_proveedor)
        {
            try
            {
                Proveedores? foundProveedores = await context.Proveedores.FindAsync(id_proveedor);
                return foundProveedores;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Proveedores>> GetAll()
        {
            try
            {
                List<Proveedores> proveedores = await context.Proveedores.ToListAsync();
                return proveedores;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Proveedores>();
            }
        }


        // Método para buscar por nombre, ciudad y país opcionalmente
        public async Task<List<Proveedores>> Buscar(string? nombre, string? ciudad, string? pais)
        {
            try
            {
                var query = context.Proveedores.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    query = query.Where(p => p.Nombre != null && p.Nombre.ToLower().Contains(nombre.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(ciudad))
                {
                    query = query.Where(p => p.Ciudad != null && p.Ciudad.ToLower().Contains(ciudad.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(pais))
                {
                    query = query.Where(p => p.PaisOrigen != null && p.PaisOrigen.ToLower().Contains(pais.ToLower()));
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Buscar: {ex.Message}");
                return new List<Proveedores>();
            }
        }

    }

    public interface IProveedoresService
    {
        Task<bool> Insert(Proveedores proveedores);
        Task<bool> Update(int id_proveedor, Proveedores proveedores);
        Task<bool> Delete(int id_proveedor);
        Task<List<Proveedores>> GetAll();
        Task<Proveedores?> GetById(int id_proveedor);
       
        Task<List<Proveedores>> Buscar(string? nombre, string? ciudad, string? pais);
    }
}
