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
