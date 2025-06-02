using Microsoft.EntityFrameworkCore;

namespace VentaDeAccesoriosAPI.Data.Configuration
{
    public static class DbConfiguration
    {
        public static void Configuration(IServiceCollection services, IConfiguration configuration) { 
            String? dbConnectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));

        }
    }
}
