var builder = WebApplication.CreateBuilder(args);

// Assure-toi d’avoir les bons packages pour OpenAPI, sinon commente cette partie
builder.Services.AddEndpointsApiExplorer(); // souvent utilisé pour Swagger/OpenAPI

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5176); // écoute sur le port 5176 dans le conteneur
});

var app = builder.Build();

app.MapHelloWorld();
app.MapWeatherForecast();

// Commente HTTPS redirection si tu n'as pas configuré HTTPS dans Docker
// app.UseHttpsRedirection();

app.Run();