using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly AppDbContext _context;

        public DetallePedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(DetallePedidosProveedores detallePedido)
        {
            try
            {
                _context.DetallePedidosProveedores.Add(detallePedido);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insert: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Update(int idDetalle, DetallePedidosProveedores detalle)
        {
            try
            {
                var foundDetail = await _context.DetallePedidosProveedores.FindAsync(idDetalle);
                if (foundDetail == null)
                    return false;

                _context.Entry(foundDetail).CurrentValues.SetValues(detalle);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int idDetalle)
        {
            try
            {
                var foundDetail = await _context.DetallePedidosProveedores.FindAsync(idDetalle);
                if (foundDetail == null)
                    return false;

                _context.DetallePedidosProveedores.Remove(foundDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false;
            }
        }

        public async Task<DetallePedidosProveedores?> GetById(int idDetalle)
        {
            try
            {
                return await _context.DetallePedidosProveedores.FindAsync(idDetalle);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el detalle de pedido por ID: " + ex.ToString());
                return null;
            }
        }

        public async Task<List<DetallePedidosProveedores>> GetAll()
        {
            try
            {
                return await _context.DetallePedidosProveedores.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todas los detalles de pedidos: " + ex.ToString());
                return new List<DetallePedidosProveedores>();
            }
        }

    }

    public interface IDetallePedidoService
    {
        Task<bool> Insert(DetallePedidosProveedores detallePedido);
        Task<bool> Update(int idDetalle, DetallePedidosProveedores detalle);
        Task<bool> Delete(int idDetalle);
        Task<DetallePedidosProveedores?> GetById(int idDetalle);
        Task<List<DetallePedidosProveedores>> GetAll();

    }
}
