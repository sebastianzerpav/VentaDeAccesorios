using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.DTO;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class PedidoProveedorService : IPedidoProveedorService
    {
        private readonly AppDbContext _context;

        public PedidoProveedorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Insert(PedidosProveedores pedido)
        {
            try
            {
                _context.PedidosProveedores.Add(pedido);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Insert: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Update(int idPedido, PedidosProveedores pedido)
        {
            try
            {
                PedidosProveedores? foundPedido = await _context.PedidosProveedores.FindAsync(idPedido);
                if (foundPedido == null) { return false; }

                _context.Entry(foundPedido).CurrentValues.SetValues(pedido);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int idPedido)
        {
            try
            {
                PedidosProveedores? foundPedido = await _context.PedidosProveedores.FindAsync(idPedido);
                if (foundPedido == null) { return false; }

                _context.PedidosProveedores.Remove(foundPedido);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Delete: {ex.Message}");
                return false;
            }
        }

        public async Task<List<PedidosProveedores>> GetAll()
        {
            try
            {
                return await _context.PedidosProveedores.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todos los pedidos: " + ex.ToString());
                return new List<PedidosProveedores>();
            }
        }

        public async Task<PedidosProveedores?> GetById(int idPedido)
        {
            try
            {
                return await _context.PedidosProveedores.FindAsync(idPedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el pedido por ID: " + ex.ToString());
                return null;
            }
        }

    }

    public interface IPedidoProveedorService
    {
        Task<bool> Insert(PedidosProveedores pedido);
        Task<bool> Update(int idPedido, PedidosProveedores pedido);
        Task<bool> Delete(int idPedido);
        Task<List<PedidosProveedores>> GetAll();
        Task<PedidosProveedores?> GetById(int idPedido);

    }
}
