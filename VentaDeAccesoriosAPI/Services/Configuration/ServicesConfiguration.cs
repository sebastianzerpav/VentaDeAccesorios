namespace VentaDeAccesoriosAPI.Services.Configuration
{
    public static class ServicesConfiguration
    {
        //Este método se invoca en program.cs
        public static void Configuration(IServiceCollection services) {
            //services.AddScoped<ITorneoService, TorneoService>(); //Ejemplo de inyección para CRUD Torneos
        }

        //Aqui puede configurarse servicio de Auth también en el futuro con otro metodo, o cualquier servicio de la app
    }
}
