using VentaDeAccesoriosAPI.Data.Models;
using VentaDeAccesoriosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Services
{
    public class TransportistasService : ITransportistasService
    {
        public readonly AppDbContext context;
        public TransportistasService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Transportista transportista)
        {
            try
            {
                context.Transportistas.Add(transportista);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_transportista, Transportista transportista)
        {
            try
            {
                Transportista? foundTransportista = await context.Transportistas.FindAsync(id_transportista);
                if (foundTransportista == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundTransportista).CurrentValues.SetValues(transportista);
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

        public async Task<bool> Delete(int id_transportista)
        {
            try
            {
                Transportista? foundTransportista = await context.Transportistas.FindAsync(id_transportista);
                if (foundTransportista == null) { return false; }
                else
                {
                    context.Transportistas.Remove(foundTransportista);
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

        public async Task<Transportista?> GetById(int id_transportista)
        {
            try
            {
                Transportista? foundTransportista = await context.Transportistas.FindAsync(id_transportista);
                return foundTransportista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public async Task<List<Transportista>> GetAll()
        {
            try
            {
                List<Transportista> transportista = await context.Transportistas.ToListAsync();
                return transportista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Transportista>();
            }
        }
    }

    public interface ITransportistasService
    {
        Task<bool> Insert(Transportista transportista);
        Task<bool> Update(int id_transportista, Transportista transportista);
        Task<bool> Delete(int id_transportista);
        Task<List<Transportista>> GetAll();
        Task<Transportista?> GetById(int id_transportista);

    }
}

