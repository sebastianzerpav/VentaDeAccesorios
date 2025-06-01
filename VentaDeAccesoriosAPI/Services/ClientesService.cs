using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class ClientesService : IClientesService
    {
        private readonly AppDbContext context;

        public ClientesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Cliente cliente)
        {
            try
            {
                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int idCliente, Cliente cliente)
        {
            try
            {
                Cliente? foundCliente = await context.Clientes.FindAsync(idCliente);
                if (foundCliente == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundCliente).CurrentValues.SetValues(cliente);
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

        public async Task<bool> Delete(int idCliente)
        {
            try
            {
                Cliente? foundCliente = await context.Clientes.FindAsync(idCliente);
                if (foundCliente == null)
                {
                    return false;
                }
                else
                {
                    context.Clientes.Remove(foundCliente);
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

        public async Task<Cliente?> GetById(int idCliente)
        {
            try
            {
                Cliente? foundCliente = await context.Clientes.FindAsync(idCliente);
                return foundCliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Cliente>> GetAll()
        {
            try
            {
                List<Cliente> clientes = await context.Clientes.ToListAsync();
                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Cliente>();
            }
        }
    }

    public interface IClientesService
    {
        Task<bool> Insert(Cliente cliente);
        Task<bool> Update(int idCliente, Cliente cliente);
        Task<bool> Delete(int idCliente);
        Task<List<Cliente>> GetAll();
        Task<Cliente?> GetById(int idCliente);
    }
}
