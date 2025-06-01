using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class OfertasService : IOfertasService
    {

        public readonly AppDbContext context;
        public OfertasService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Insert(Oferta oferta)
        {
            try
            {
                // 1. Verificar si hay productos en la base de datos
                bool hayProductos = await context.Productos.AnyAsync();
                if (!hayProductos)
                {
                    Console.WriteLine("No hay productos registrados. No se puede agregar la oferta.");
                    return false;
                }

                // 2. Verificar que el producto especificado en la oferta exista
                bool productoExiste = await context.Productos.AnyAsync(p => p.IdProducto == oferta.IdProducto);
                if (!productoExiste)
                {
                    Console.WriteLine($"El producto con ID {oferta.IdProducto} no existe.");
                    return false;
                }
                // 3. Verificar que la fecha de inicio sea anterior a la fecha de fin
                if (oferta.FechaInicio.HasValue && oferta.FechaFin.HasValue && oferta.FechaInicio > oferta.FechaFin)
                {
                    Console.WriteLine("La fecha de inicio debe ser anterior a la fecha de fin.");
                    return false;
                }
                context.Ofertas.Add(oferta);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_oferta, Oferta oferta)
        {
            try
            {
                Oferta? foundOferta = await context.Ofertas.FindAsync(oferta);
                if (foundOferta == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundOferta).CurrentValues.SetValues(oferta);
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

        public async Task<bool> Delete(int id_oferta)
        {
            try
            {
                Oferta? foundOferta = await context.Ofertas.FindAsync(id_oferta);
                if (foundOferta == null) { return false; }
                else
                {
                    context.Ofertas.Remove(foundOferta);
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

        public async Task<Oferta?> GetById(int id_oferta)
        {
            try
            {
                Oferta? foundOferta = await context.Ofertas.FindAsync(id_oferta);
                return foundOferta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }
        public async Task<List<Oferta>> GetAll()
        {
            try
            {
                List<Oferta> ofertas = await context.Ofertas.ToListAsync();
                return ofertas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Oferta>();
            }
        }
    }

    public interface IOfertasService
    {
        Task<bool> Insert(Oferta oferta);
        Task<bool> Update(int id_oferta, Oferta oferta);
        Task<bool> Delete(int id_oferta);
        Task<List<Oferta>> GetAll();
        Task<Oferta?> GetById(int id_oferta);

    }
}