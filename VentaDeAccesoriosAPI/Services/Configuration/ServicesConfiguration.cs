using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static VentaDeAccesoriosAPI.Services.DetalleVentaService;

namespace VentaDeAccesoriosAPI.Services.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configuration(IServiceCollection services)
        {

            services.AddScoped<IProductosService, ProductosService>();
            services.AddScoped<IProveedoresService, ProveedoresService>();
            services.AddScoped<ISedesService, SedesService>();
            services.AddScoped<IUsuarioService, UsuariosService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOfertasService, OfertasService>();
            services.AddScoped<ITransportistasService, TransportistasService>();
            services.AddScoped<IVentasService, VentasService>();
            services.AddScoped<IDetalleVentaService, DetalleVentaService>();
            services.AddScoped<IEnviosService, EnviosService>();
            services.AddScoped<IClientesService, ClientesService>();
            services.AddScoped<IRolesService, RolesService>();
            //services.AddScoped<IPermisosService, PermisosService>();
            services.AddScoped<IUsuariosRolesService, UsuariosRolesService>();
           // services.AddScoped<IRolesPermisosService, RolesPermisosService>();
            services.AddScoped<IImagenProductoService, ImagenProductoService>();
            services.AddScoped<IGarantiaService, GarantiaService>();



        }

        public static void AuthConfiguration(IConfiguration configuration, IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JwtConfiguration:Key"]);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();
        }
    }
}