using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Endpoints;

public static class InventoryHandlers
{
    public static async Task<IEnumerable<Inventory>> GetInventories(HomeInventoryDbContext db)
    {
        return await db.Inventories.ToListAsync();
    }

    public static async Task<IResult> GetInventoryById(HomeInventoryDbContext db, int id)
    { 
        var inventory = await db.Inventories.FindAsync(id);
        return inventory is not null ? Results.Ok(inventory) : Results.NotFound();
    }
    
   public static  async Task<IResult>  AddInventoryWithAddress(HomeInventoryDbContext db, InventoryWithAddressDto inventoryWithAddressDto)
    {
        AdressLocation location = new AdressLocation
        {
            Name = inventoryWithAddressDto.LocationName,
            Adress = inventoryWithAddressDto.Address
        };
        
        Inventory inventory = new()
        {
            Name = inventoryWithAddressDto.Name, 
            Description = inventoryWithAddressDto.Description,
            Possible = inventoryWithAddressDto.Possible,
            Location = location
        };
        
        db.Inventories.Add(inventory);
        await db.SaveChangesAsync();
        return Results.Created($"/inventory/{inventory.Id}", inventory);
    }
   
    public static async Task<IResult> AddInventoryWithCoordinates(HomeInventoryDbContext db, InventoryWithCoordinatesDto inventoryWithCoordinatesDto)
    {
        CoordinateLocation coordinateLocation = new()
        {
            Name = inventoryWithCoordinatesDto.LocationName,
            X = inventoryWithCoordinatesDto.X,
            Y = inventoryWithCoordinatesDto.Y
        };
        Inventory inventory = new()
        {
            Name = inventoryWithCoordinatesDto.Name,
            Description = inventoryWithCoordinatesDto.Description,
            Possible = inventoryWithCoordinatesDto.Possible,
            Location = coordinateLocation
        };
        
        db.Inventories.Add(inventory);
        await db.SaveChangesAsync();
        return Results.Created($"/inventory/{inventory.Id}", inventory);
        
    }
    public static async Task<IResult> UpdateInventory(HomeInventoryDbContext db, int id, Inventory updatedInventory)
    {
        var inventory = await db.Inventories.FindAsync(id);
        if (inventory is null) return Results.NotFound();

        inventory.Name = updatedInventory.Name;
        inventory.Possible = updatedInventory. Possible;

        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    
    public static async Task<IResult> DeleteInventory (HomeInventoryDbContext db, int id)
    {
        var inventory = await db.Inventories.FindAsync(id);
        if (inventory is null) return Results.NotFound();

        db.Inventories.Remove(inventory);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
}
public record InventoryDto(string Name, string Description,double Possible);
public record InventoryWithAddressDto(string Name, string Description,double Possible,string LocationName,string Address):InventoryDto(Name,Description,Possible);
public record InventoryWithCoordinatesDto(string Name, string Description, double Possible, string LocationName,double X, double Y):InventoryDto(Name,Description,Possible);
