using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(Producto producto)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(producto.Nombre))
                    return false;

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Considera usar un logger en lugar de Console.WriteLine
                Console.WriteLine($"Error en Insert: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Update(int id_producto, Producto producto)
        {
            try
            {
                var foundProducto = await _context.Productos.FindAsync(id_producto);
                if (foundProducto == null)
                    return false;

                // Actualizar campos específicos para evitar sobrescribir el ID
                foundProducto.Nombre = producto.Nombre;
                foundProducto.Descripcion = producto.Descripcion;
                foundProducto.PrecioCompra = producto.PrecioCompra;
                foundProducto.PrecioVenta = producto.PrecioVenta;
                foundProducto.StockTotal = producto.StockTotal;
                foundProducto.GarantiaMeses = producto.GarantiaMeses;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int id_producto)
        {
            try
            {
                var foundProducto = await _context.Productos.FindAsync(id_producto);
                if (foundProducto == null)
                    return false;

                _context.Productos.Remove(foundProducto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Producto>> GetAll()
        {
            try
            {
                return await _context.Productos.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAll: {ex.Message}");
                return new List<Producto>();
            }
        }

        public async Task<Producto?> GetById(int id_producto)
        {
            try
            {
                return await _context.Productos.FindAsync(id_producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetById: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Producto>> GetByNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return new List<Producto>();

                return await _context.Productos
                    .Include(p => p.ComentariosClientes)
                    .Include(p => p.DetallePedidosProveedores)
                    .Include(p => p.DetalleVenta)
                    .Include(p => p.Garantia)
                    //.Include(p => p.HistorialStocks)
                    //.Include(p => p.MovimientosInventarios)
                    //.Include(p => p.Oferta)
                   // .Include(p => p.ProductosProveedores)
                    .Where(p => p.Nombre != null && p.Nombre.ToLower().Contains(nombre.ToLower()))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetByNombre: {ex.Message}");
                return new List<Producto>();
            }
        }
    }

    public interface IProductoService
    {
        Task<bool> Insert(Producto producto);
        Task<bool> Update(int id_producto, Producto producto);
        Task<bool> Delete(int id_producto);
        Task<List<Producto>> GetAll();
        Task<Producto?> GetById(int id_producto);
        Task<List<Producto>> GetByNombre(string nombre);
    }
}