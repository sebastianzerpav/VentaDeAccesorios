using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VentaDeAccesoriosAPI.Services;

namespace VentaDeAccesoriosAPI.Services.Configuration
{
        public static class ServicesConfiguration
        {
            public static void Configuration(IServiceCollection services)
            {
            services.AddScoped<IUsuarioService, UsuariosService>();//User service
            services.AddScoped<IProveedoresService, ProveedoresService>();//proveedores service
            services.AddScoped<ITransportistasService, TransportistasService>();//Transportistas service
            services.AddScoped<IOfertaService, OfertaService>();//Oferta service
            services.AddScoped<IEnvioService, EnvioService>();//Envio service
            services.AddScoped<IVentaService, VentaService>();//Oferta service
            services.AddScoped<ISedesService, SedesService>();//Sedes service
            services.AddScoped<IAuthService, AuthService>(); //Auth service
            }

            //Auth configuration
            public static void AuthConfiguration(IConfiguration configuration, IServiceCollection services)
            {
                string key = configuration.GetValue<string>("JwtConfiguration:Key")!;
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                services.AddAuthentication(configuration =>
                {
                    configuration.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    configuration.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(configuration => {
                    configuration.RequireHttpsMetadata = false;
                    configuration.SaveToken = true;
                    configuration.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
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
