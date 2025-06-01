using Microsoft.AspNetCore.Builder;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

public static class ItemsEndpoint
{
    public static void MapItems(this WebApplication app)
    {
        app.MapGet("/items", async (IConfiguration config) =>
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var items = await connection.QueryAsync("SELECT * FROM items");

            return Results.Ok(items);
        });
    }
}