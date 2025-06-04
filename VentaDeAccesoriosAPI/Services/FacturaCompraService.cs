using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class FacturaCompraService : IFacturaCompraService
    {
        private readonly AppDbContext _context;

        public FacturaCompraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(FacturasCompra factura)
        {
            try
            {
                // 1. Obtener todos los detalles para el IdPedido de la factura
                if (factura.IdPedido.HasValue)
                {
                    var detalles = await _context.DetallePedidosProveedores
                        .Where(d => d.IdPedido == factura.IdPedido.Value)
                        .ToListAsync();

                    decimal totalCalc = 0m;

                    foreach (var det in detalles)
                    {
                        if (!det.IdProducto.HasValue)
                            continue;

                        // 2. Obtener el producto para sacar el PrecioCompra
                        var producto = await _context.Productos.FindAsync(det.IdProducto.Value);
                        if (producto == null)
                            continue;

                        // 3. Asignar el PrecioUnitario en el detalle
                        det.PrecioUnitario = producto.PrecioCompra;

                        // 4. Calcular subtotal de esa línea (cantidad * precio)
                        var cantidad = det.Cantidad.GetValueOrDefault(0);
                        var precioUnit = producto.PrecioCompra.GetValueOrDefault(0m);

                        totalCalc += cantidad * precioUnit;
                    }

                    // 5. Asignar el total calculado a la factura
                    factura.Total = totalCalc;

                    // 6. Guardar cambios en los detalles para persistir el PrecioUnitario
                    if (detalles.Any())
                    {
                        _context.DetallePedidosProveedores.UpdateRange(detalles);
                        await _context.SaveChangesAsync();
                    }
                }

                // 7. Insertar la factura ya con el Total calculado
                _context.FacturasCompras.Add(factura);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insert: {ex.Message}");
                return false;
            }
        }

        
        public async Task<bool> Update(int idFactura, FacturasCompra factura)
        {
            try
            {
                var foundFactura = await _context.FacturasCompras.FindAsync(idFactura);
                if (foundFactura == null)
                    return false;

                // 1. Volver a calcular Total (en caso de que cambie IdPedido o sus detalles)
                decimal totalCalc = 0m;

                if (factura.IdPedido.HasValue)
                {
                    var detalles = await _context.DetallePedidosProveedores
                        .Where(d => d.IdPedido == factura.IdPedido.Value)
                        .ToListAsync();

                    foreach (var det in detalles)
                    {
                        if (!det.IdProducto.HasValue)
                            continue;

                        var producto = await _context.Productos.FindAsync(det.IdProducto.Value);
                        if (producto == null)
                            continue;

                        // Asignar el PrecioUnitario actualizado
                        det.PrecioUnitario = producto.PrecioCompra;

                        // Calcular subtotal de esta línea
                        var cantidad = det.Cantidad.GetValueOrDefault(0);
                        var precioUnit = producto.PrecioCompra.GetValueOrDefault(0m);

                        totalCalc += cantidad * precioUnit;
                    }

                    // Guardar cambios en los detalles para persistir el PrecioUnitario actualizado
                    if (detalles.Any())
                    {
                        _context.DetallePedidosProveedores.UpdateRange(detalles);
                        await _context.SaveChangesAsync();
                    }

                    factura.Total = totalCalc;
                }

                // 2. Reemplazar valores de la factura encontrada por los de 'factura'
                _context.Entry(foundFactura).CurrentValues.SetValues(factura);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int idFactura)
        {
            try
            {
                var foundFactura = await _context.FacturasCompras.FindAsync(idFactura);
                if (foundFactura == null)
                    return false;

                _context.FacturasCompras.Remove(foundFactura);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false;
            }
        }

        public async Task<FacturasCompra?> GetById(int idFactura)
        {
            try
            {
                return await _context.FacturasCompras.FindAsync(idFactura);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la factura por ID: " + ex.ToString());
                return null;
            }
        }

        public async Task<List<FacturasCompra>> GetAll()
        {
            try
            {
                return await _context.FacturasCompras.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todas las facturas: " + ex.ToString());
                return new List<FacturasCompra>();
            }
        }
    }

    public interface IFacturaCompraService
    {
        Task<bool> Insert(FacturasCompra factura);
        Task<bool> Update(int idFactura, FacturasCompra factura);
        Task<bool> Delete(int idFactura);

        Task<FacturasCompra?> GetById(int idFactura);
        Task<List<FacturasCompra>> GetAll();
    }
}
