using VentaDeAccesoriosAPI.Data.Configuration;
using VentaDeAccesoriosAPI.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Db Connection
DbConfiguration.Configuration(builder.Services, builder.Configuration);
//Authentication and Authorization Configuration
ServicesConfiguration.AuthConfiguration(builder.Configuration, builder.Services);
//Services
ServicesConfiguration.Configuration(builder.Services);

//Services configuration 
ServicesConfiguration.Configuration(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
