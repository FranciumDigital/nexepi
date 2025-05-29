using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NexepiApi.Data;

public static class ItemsEndpoint
{
    public static void MapItems(this WebApplication app)
    {
        app.MapGet("/items", async (ApplicationDbContext db) =>
        {
            var allItems = await db.Items.ToListAsync();
            return Results.Ok(allItems);
        });
    }
}
