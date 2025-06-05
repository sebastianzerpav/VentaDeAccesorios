using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class VentasService : IVentasService
    {
        private readonly AppDbContext context;

        public VentasService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Venta venta)
        {
            try
            {
                // 1. Verificar existencia de Cliente
                if (!await context.Clientes.AnyAsync(c => c.IdCliente == venta.IdCliente))
                {
                    Console.WriteLine($"Cliente con ID {venta.IdCliente} no existe.");
                    return false;
                }

                // 2. Verificar existencia de Usuario
                if (!await context.Usuarios.AnyAsync(u => u.IdUsuario == venta.IdUsuario))
                {
                    Console.WriteLine($"Usuario con ID {venta.IdUsuario} no existe.");
                    return false;
                }

                // 3. Verificar existencia de Sede
                if (!await context.Sedes.AnyAsync(s => s.IdSede == venta.IdSede))
                {
                    Console.WriteLine($"Sede con ID {venta.IdSede} no existe.");
                    return false;
                }

                // 4. Validar que la fecha no sea futura
                if (venta.FechaVenta.HasValue && venta.FechaVenta.Value > DateOnly.FromDateTime(DateTime.Now))
                {
                    Console.WriteLine("La fecha de venta no puede ser futura.");
                    return false;
                }


                context.Ventas.Add(venta);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar venta: " + ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_venta, Venta venta)
        {
            try
            {
                Venta? foundVenta = await context.Ventas.FindAsync(id_venta);
                if (foundVenta == null)
                {
                    Console.WriteLine($"Venta con ID {id_venta} no existe.");
                    return false;
                }

                context.Entry(foundVenta).CurrentValues.SetValues(venta);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar venta: " + ex.ToString());
                return false;
            }
        }

        public async Task<bool> Delete(int id_venta)
        {
            try
            {
                var foundVenta = await context.Ventas.FindAsync(id_venta);
                if (foundVenta == null)
                    return false;

                var entry = context.Entry(foundVenta);
                foreach (var collection in entry.Collections)
                {
                    await collection.LoadAsync();
                    (collection.CurrentValue as System.Collections.IList)?.Clear();
                }

                context.Ventas.Remove(foundVenta);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar venta: " + ex.ToString());
                return false;
            }
        }

        public async Task<Venta?> GetById(int id_venta)
        {
            try
            {
                return await context.Ventas.FindAsync(id_venta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener venta por ID: " + ex.ToString());
                return null;
            }
        }

        public async Task<List<Venta>> GetAll()
        {
            try
            {
                return await context.Ventas.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todas las ventas: " + ex.ToString());
                return new List<Venta>();
            }
        }

        [HttpGet("GetUltimo")]
        public async Task<Venta> GetUltimo()
        {
            Venta? ultimo = await context.Ventas.OrderByDescending(v => v.IdVenta).FirstOrDefaultAsync();

            return ultimo;
        }
    }

    public interface IVentasService
    {
        Task<bool> Insert(Venta venta);
        Task<bool> Update(int id_venta, Venta venta);
        Task<bool> Delete(int id_venta);
        Task<List<Venta>> GetAll();
        Task<Venta?> GetById(int id_venta);
        Task<Venta> GetUltimo();
    }
}
