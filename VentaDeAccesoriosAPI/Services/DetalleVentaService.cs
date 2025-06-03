using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using static VentaDeAccesoriosAPI.Services.DetalleVentaService;

namespace VentaDeAccesoriosAPI.Services
{
    public class DetalleVentaService : IDetalleVentaService
    {
        public readonly AppDbContext context;
        public DetalleVentaService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(DetalleVenta detalle)
        {
            try
            {
                // 1. Verificar que la venta especificada exista
                bool ventaExiste = await context.Ventas.AnyAsync(p => p.IdVenta == detalle.IdVenta);
                if (!ventaExiste)
                {
                    Console.WriteLine($"La venta con ID {detalle.IdVenta} no existe.");
                    return false;
                }

                // 2. Obtener el producto
                var producto = await context.Productos.FirstOrDefaultAsync(p => p.IdProducto == detalle.IdProducto);
                if (producto == null)
                {
                    Console.WriteLine($"El producto con ID {detalle.IdProducto} no existe.");
                    return false;
                }

                
                var hoy = DateOnly.FromDateTime(DateTime.Now);

                // 4. Verificar si hay una oferta activa
                var oferta = await context.Ofertas.FirstOrDefaultAsync(o =>
                    o.IdProducto == detalle.IdProducto &&
                    o.FechaInicio <= hoy &&
                    o.FechaFin >= hoy);

                // 5. Asignar el precio según si hay oferta activa
                detalle.PrecioUnitario = oferta != null ? oferta.PrecioPromocional : producto.PrecioVenta;

                // 6. Calcular subtotal
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

                // 7. Guardar en base de datos
                context.DetalleVentas.Add(detalle);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar detalle de venta: {ex}");
                return false;
            }
        }

        public async Task<bool> Update(int id_detalle, DetalleVenta detalle)
        {
            try
            {
                DetalleVenta? foundDetalle = await context.DetalleVentas.FindAsync(id_detalle);
                if (foundDetalle == null)
                {
                    Console.WriteLine($"No se encontró el detalle con ID {id_detalle}.");
                    return false;
                }

                // Verificar que la venta exista
                bool ventaExiste = await context.Ventas.AnyAsync(p => p.IdVenta == detalle.IdVenta);
                if (!ventaExiste)
                {
                    Console.WriteLine($"La venta con ID {detalle.IdVenta} no existe.");
                    return false;
                }

                // Verificar que el producto exista
                var producto = await context.Productos.FirstOrDefaultAsync(p => p.IdProducto == detalle.IdProducto);
                if (producto == null)
                {
                    Console.WriteLine($"El producto con ID {detalle.IdProducto} no existe.");
                    return false;
                }

                // Validar cantidad
                if (detalle.Cantidad == null || detalle.Cantidad <= 0)
                {
                    Console.WriteLine("La cantidad debe ser mayor a 0.");
                    return false;
                }

                // Obtener la fecha actual como DateOnly
                var hoy = DateOnly.FromDateTime(DateTime.Now);

                // Verificar si hay oferta activa
                var oferta = await context.Ofertas.FirstOrDefaultAsync(o =>
                    o.IdProducto == detalle.IdProducto &&
                    o.FechaInicio <= hoy &&
                    o.FechaFin >= hoy);

                // Asignar precio según si hay oferta activa
                detalle.PrecioUnitario = oferta != null ? oferta.PrecioPromocional: producto.PrecioVenta;

                // Calcular subtotal
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

                // Actualizar el registro en la base de datos
                context.Entry(foundDetalle).CurrentValues.SetValues(detalle);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar detalle de venta: {ex}");
                return false;
            }
        }




        public async Task<bool> Delete(int id_detalle)
        {
            try
            {
                var foundDetalle = await context.DetalleVentas.FindAsync(id_detalle);
                if (foundDetalle == null)
                    return false;

                var entry = context.Entry(foundDetalle);
                foreach (var collection in entry.Collections)
                {
                    await collection.LoadAsync();
                    (collection.CurrentValue as System.Collections.IList)?.Clear();
                }

                context.DetalleVentas.Remove(foundDetalle);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<DetalleVenta?> GetById(int id_detalle)
        {
            try
            {
                DetalleVenta? foundDetalle = await context.DetalleVentas.FindAsync(id_detalle);
                return foundDetalle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public async Task<List<DetalleVenta>> GetAll()
        {
            try
            {
                List<DetalleVenta> detalles = await context.DetalleVentas.ToListAsync();
                return detalles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<DetalleVenta>();
            }
        }

        public interface IDetalleVentaService
        {
            Task<bool> Insert(DetalleVenta detalle);
            Task<bool> Update(int id_detalle, DetalleVenta detalle);
            Task<bool> Delete(int id_detalle);
            Task<List<DetalleVenta>> GetAll();
            Task<DetalleVenta?> GetById(int id_detalle);

        }
    }
}
