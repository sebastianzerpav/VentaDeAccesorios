using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Services
{
    public class UsuariosService : IUsuarioService
    {
        public readonly AppDbContext context;
        private readonly IAuthService authService;
        public UsuariosService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Insert(Usuario usuario)
        {
            try
            {
                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int id_usuario, Usuario usuario)
        {
            try
            {
                Usuario? foundUsuario = await context.Usuarios.FindAsync(id_usuario);
                if (foundUsuario == null)
                {
                    return false;
                }
                else
                {
                    context.Entry(foundUsuario).CurrentValues.SetValues(usuario);
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

        public async Task<bool> Delete(int id_usuario)
        {
            try
            {
                var foundUsuario = await context.Usuarios.FindAsync(id_usuario);
                if (foundUsuario == null)
                    return false;

                var entry = context.Entry(foundUsuario);
                foreach (var collection in entry.Collections)
                {
                    await collection.LoadAsync();
                    (collection.CurrentValue as System.Collections.IList)?.Clear();
                }

                context.Usuarios.Remove(foundUsuario);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<Usuario?> GetById(int id_usuario)
        {
            try
            {
                Usuario? foundUsuario = await context.Usuarios.FindAsync(id_usuario);
                return foundUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<Usuario>> GetByName(string nombre)
        {
            try
            {
                return await context.Usuarios
                    .Where(u => u.Nombre.Contains(nombre)) // búsqueda parcial
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Usuario>();
            }
        }

        public async Task<List<Usuario>> GetAll()
        {
            try
            {
                List<Usuario> usuarios = await context.Usuarios.ToListAsync();
                return usuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Usuario>();
            }
        }

    }
    public interface IUsuarioService
    {
        Task<bool> Insert(Usuario usuario);
        Task<bool> Update(int id_usuario, Usuario usuario);
        Task<bool> Delete(int id_usuario);
        Task<List<Usuario>> GetAll();
        Task<Usuario?> GetById(int id_usuario);
        Task<List<Usuario>> GetByName(string nombre);

    }
}
