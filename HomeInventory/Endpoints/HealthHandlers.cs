using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Endpoints;

public static class HealthHandlers
{
    public static async Task<IResult> Health (HomeInventoryDbContext db)
    {
        await  db.Inventories.ToListAsync();
        return Results.Ok();
    } 
}