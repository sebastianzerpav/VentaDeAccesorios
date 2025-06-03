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
                bool VentaExiste = await context.Ventas.AnyAsync(p => p.IdVenta== detalle.IdVenta);
                if (!VentaExiste)
                {
                    Console.WriteLine($"La venta con ID {detalle.IdVenta} no existe.");
                    return false;
                }

                // 2. Verificar que el producto especificado en la oferta exista
                bool productoExiste = await context.Productos.AnyAsync(p => p.IdProducto == detalle.IdProducto);
                if (!productoExiste)
                {
                    Console.WriteLine($"El producto con ID {detalle.IdProducto} no existe.");
                    return false;
                }

                context.DetalleVentas.Add(detalle);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_detalle, DetalleVenta detalle)
        {
            try
            {
                DetalleVenta? FoundDetalleVenta = await context.DetalleVentas.FindAsync(id_detalle);
                if (FoundDetalleVenta == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(FoundDetalleVenta).CurrentValues.SetValues(detalle);
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

        public async Task<bool> Delete(int id_detalle)
        {
            try
            {
                DetalleVenta? FoundDetalle = await context.DetalleVentas.FindAsync(id_detalle);
                if (FoundDetalle == null) { return false; }
                else
                {
                    context.DetalleVentas.Remove(FoundDetalle);
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
