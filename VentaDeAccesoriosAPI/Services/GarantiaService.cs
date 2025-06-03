using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class GarantiaService : IGarantiaService
    {
        private readonly AppDbContext _context;

        public GarantiaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Garantia>> ObtenerGarantias()
        {
            return await _context.Garantia
                .Include(g => g.IdVentaNavigation)
                .Include(g => g.IdProductoNavigation)
                .Include(g => g.AtencionesGarantia)
                .ToListAsync();
        }

        public async Task<Garantia?> ObtenerGarantiaPorId(int id)
        {
            return await _context.Garantia
                .Include(g => g.IdVentaNavigation)
                .Include(g => g.IdProductoNavigation)
                .Include(g => g.AtencionesGarantia)
                .FirstOrDefaultAsync(g => g.IdGarantia == id);
        }

        public async Task<Garantia> CrearGarantia(Garantia garantia)
        {
            _context.Garantia.Add(garantia);
            await _context.SaveChangesAsync();
            return garantia;
        }

        public async Task<bool> ActualizarGarantia(int id, Garantia garantiaActualizada)
        {
            var garantia = await _context.Garantia.FindAsync(id);
            if (garantia == null) return false;

            garantia.IdVenta = garantiaActualizada.IdVenta;
            garantia.IdProducto = garantiaActualizada.IdProducto;
            garantia.FechaInicio = garantiaActualizada.FechaInicio;
            garantia.FechaFin = garantiaActualizada.FechaFin;
            garantia.Estado = garantiaActualizada.Estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarGarantia(int id)
        {
            try
            {
                var foundGarantia = await _context.Garantia.FindAsync(id);
                if (foundGarantia == null)
                    return false;

                var entry = _context.Entry(foundGarantia);
                foreach (var collection in entry.Collections)
                {
                    await collection.LoadAsync();
                    (collection.CurrentValue as System.Collections.IList)?.Clear();
                }

                _context.Garantia.Remove(foundGarantia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }

    public interface IGarantiaService
    {
        Task<List<Garantia>> ObtenerGarantias();
        Task<Garantia?> ObtenerGarantiaPorId(int id);
        Task<Garantia> CrearGarantia(Garantia garantia);
        Task<bool> ActualizarGarantia(int id, Garantia garantiaActualizada);
        Task<bool> EliminarGarantia(int id);
    }
}
