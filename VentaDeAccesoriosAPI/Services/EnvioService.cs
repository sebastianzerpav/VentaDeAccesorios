using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace VentaDeAccesoriosAPI.Services
{
    public class EnvioService : IEnvioService
    {
        public readonly AppDbContext context;
        public EnvioService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Envio envio)
        {
            try
            {
                // 1. Verificar que la venta especificada en el envío exista
                bool ventaExiste = await context.Ventas.AnyAsync(p => p.IdVenta == envio.IdVenta);
                if (!ventaExiste)
                {
                    Console.WriteLine($"La Venta con ID {envio.IdVenta} no existe.");
                    return false;
                }
                // 2. Verificar que el transportista especificado en el envío exista
                bool transportistaExiste = await context.Transportistas.AnyAsync(p => p.IdTransportista == envio.IdTransportista);
                if (!transportistaExiste)
                {
                    Console.WriteLine($"El Transportista con ID {envio.IdTransportista} no existe.");
                    return false;
                }
                context.Envios.Add(envio);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_envio, Envio envio)
        {
            try
            {
                Envio? foundEnvio = await context.Envios.FindAsync(id_envio);
                if (foundEnvio == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundEnvio).CurrentValues.SetValues(envio);
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

        public async Task<bool> Delete(int id_envio)
        {
            try
            {
                Envio? foundEnvio = await context.Envios.FindAsync(id_envio);
                if (foundEnvio == null)
                {
                    return false;
                }
                else
                {
                    context.Envios.Remove(foundEnvio);
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

        public async Task<Envio?> GetById(int id_envio)
        {
            try
            {
                Envio? foundEnvio = await context.Envios.FindAsync(id_envio);
                return foundEnvio;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Envio>> GetAll()
        {
            try
            {
                List<Envio> envios = await context.Envios.ToListAsync();
                return envios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Envio>();
            }
        }
    }
    public interface IEnvioService
    {
        Task<bool> Insert(Envio envio);
        Task<bool> Update(int id_envio, Envio envio);
        Task<bool> Delete(int id_envio);
        Task<List<Envio>> GetAll();
        Task<Envio?> GetById(int id_envio);

    }
}
