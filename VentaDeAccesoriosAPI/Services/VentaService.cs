using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class VentaService : IVentaService
    {

        public readonly AppDbContext context;
        public VentaService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Venta venta)
        {
            try
            {
                // 1. Verificar que el cliente especificado en la venta exista
                bool clienteExiste = await context.Clientes.AnyAsync(p => p.IdCliente == venta.IdCliente);
                if (!clienteExiste)
                {
                    Console.WriteLine($"El Cliente con ID {venta.IdCliente} no existe.");
                    return false;
                }

                // 2. Verificar que el Usuario especificado en la venta exista
                bool usuarioExiste = await context.Usuarios.AnyAsync(p => p.IdUsuario == venta.IdUsuario);
                if (!usuarioExiste)
                {
                    Console.WriteLine($"El Usuario con ID {venta.IdUsuario} no existe.");
                    return false;
                }

                // 3. Verificar que la Sede especificada en la venta exista
                bool sedeExiste = await context.Sedes.AnyAsync(p => p.IdSede == venta.IdSede);
                if (!sedeExiste)
                {
                    Console.WriteLine($"La sede con ID {venta.IdSede} no existe.");
                    return false;
                }
                // 4. Verificar que la fecha de venta sea válida
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
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_venta, Venta venta)
        {
            try
            {
                Venta? foundVenta = await context.Ventas.FindAsync(venta);
                if (foundVenta == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundVenta).CurrentValues.SetValues(venta);
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

        public async Task<bool> Delete(int id_venta)
        {
            try
            {
                Venta? foundVenta = await context.Ventas.FindAsync(id_venta);
                if (foundVenta == null) { return false; }
                else
                {
                    context.Ventas.Remove(foundVenta);
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

        public async Task<Venta?> GetById(int id_venta)
        {
            try
            {
                Venta? foundVenta = await context.Ventas.FindAsync(id_venta);
                return foundVenta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public async Task<List<Venta>> GetAll()
        {
            try
            {
                List<Venta> ventas = await context.Ventas.ToListAsync();
                return ventas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Venta>();
            }
        }
    }
    public interface IVentaService
    {
        Task<bool> Insert(Venta venta);
        Task<bool> Update(int id_venta, Venta venta);
        Task<bool> Delete(int id_venta);
        Task<List<Venta>> GetAll();
        Task<Venta?> GetById(int id_venta);

    }
}
