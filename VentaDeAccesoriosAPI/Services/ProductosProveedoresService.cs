using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class ProductosProveedoresService : IProductosProveedoresService
    {
        private readonly AppDbContext _context;

        public ProductosProveedoresService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(ProductosProveedores relacion)
        {
            try
            {
                _context.ProductosProveedores.Add(relacion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insert: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Update(int id_relacion, ProductosProveedores relacion)
        {
            try
            {
                var foundRelation = await _context.ProductosProveedores.FindAsync(id_relacion);
                if (foundRelation == null)
                    return false;

                _context.Entry(foundRelation).CurrentValues.SetValues(relacion);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int id_relacion)
        {
            try
            {
                var foundRelation = await _context.ProductosProveedores.FindAsync(id_relacion);
                if (foundRelation == null)
                    return false;

                _context.ProductosProveedores.Remove(foundRelation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ProductosProveedoresNaN>> GetAll()
        {
            try
            {
                List<ProductosProveedores> listaRelaciones = await _context.ProductosProveedores.ToListAsync();

                List<ProductosProveedoresNaN> listaRetorno = new List<ProductosProveedoresNaN>();
                foreach (ProductosProveedores relacion in listaRelaciones)
                {
                    Producto? productoEncontrado = await _context.Productos.FindAsync(relacion.IdProducto);
                    Proveedores? proveedorEncontrado = await _context.Proveedores.FindAsync(relacion.IdProveedor);
                    if (productoEncontrado == null || proveedorEncontrado == null) { continue; }
                    else
                    {
                        listaRetorno.Add(new ProductosProveedoresNaN(productoEncontrado, proveedorEncontrado));
                    }
                }


                return listaRetorno;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAll: {ex.Message}");
                return new List<ProductosProveedoresNaN>();
            }
        }

        public async Task<ProductosProveedoresNaN?> GetById(int id_relacion)
        {
            try
            {
                ProductosProveedores? relacionEncontrada = await _context.ProductosProveedores.FindAsync(id_relacion);
                if (relacionEncontrada == null) { return null; } else {
                    Producto? producto = await _context.Productos.FindAsync(relacionEncontrada.IdProducto);
                    Proveedores? proveedor = await _context.Proveedores.FindAsync(relacionEncontrada.IdProveedor);
                    if (proveedor == null || producto == null) { return null; } else {
                        ProductosProveedoresNaN retorno = new ProductosProveedoresNaN(producto,proveedor);
                        return retorno;
                    }
                }
                   
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetById: {ex.Message}");
                return null;
            }
        }

    }

    public interface IProductosProveedoresService
    {
        Task<bool> Insert(ProductosProveedores relacion);
        Task<bool> Update(int id_relacion, ProductosProveedores relacion);
        Task<bool> Delete(int id_relacion);
        Task<List<ProductosProveedoresNaN>> GetAll();
        Task<ProductosProveedoresNaN?> GetById(int id_relacion);
    }
}
