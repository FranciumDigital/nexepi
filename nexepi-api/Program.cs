using NexepiApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Assure-toi d’avoir les bons packages pour OpenAPI, sinon commente cette partie
builder.Services.AddEndpointsApiExplorer(); // souvent utilisé pour Swagger/OpenAPI

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(11, 8)))); // ← adapte à ta version de MariaDB

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5176); // écoute sur le port 5176 dans le conteneur
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Adresse de ton front Vue
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapItems();
app.MapHelloWorld();
app.MapWeatherForecast();

// Commente HTTPS redirection si tu n'as pas configuré HTTPS dans Docker
// app.UseHttpsRedirection();

app.Run();