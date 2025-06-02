using VentaDeAccesoriosAPI.Data.Configuration;
using VentaDeAccesoriosAPI.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Db Connection
DbConfiguration.Configuration(builder.Services, builder.Configuration);

// Authentication and Authorization Configuration
ServicesConfiguration.AuthConfiguration(builder.Configuration, builder.Services);

// Services
ServicesConfiguration.Configuration(builder.Services);

builder.Services.AddControllers();

// Swagger/OpenAPI con configuración para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT con el prefijo 'Bearer '",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // Muy importante para activar la autenticación
app.UseAuthorization();

app.MapControllers();

app.Run();