using DotNetEnv;
using NexepiApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Assure-toi d’avoir les bons packages pour OpenAPI, sinon commente cette partie
builder.Services.AddEndpointsApiExplorer(); // souvent utilisé pour Swagger/OpenAPI

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