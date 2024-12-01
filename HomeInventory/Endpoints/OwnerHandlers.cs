using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Endpoints;

public static  class OwnerHandlers
{
    public  static async Task<IResult> AddOwner(HomeInventoryDbContext db,AddOwnerRequest request)
    {
        Owner owner = new Owner()
        {
            Name = request.FirstName, 
            Surname = request.LastName,
            Email = request.Email,
        };
        
        db.Owners.Add(owner);
        await db.SaveChangesAsync();

        return Results.Created();
    }

    public static async Task<IResult> GetOwnerWithId(HomeInventoryDbContext db, int id)
    {
        var owner = await db.Owners
            .Include(o => o.Inventories)
            .FirstOrDefaultAsync(o => o.Id == id);

        List<InventoryDtoWithoutOwner> inventoriesWithoutOwner = new();
        
        foreach (Inventory inventory in owner.Inventories)
        {
            InventoryDtoWithoutOwner inventoryDtoWithoutOwner = new (
                Id:inventory.Id,
                Name:inventory.Name,
                Description:inventory.Description,
                Location:inventory.Location
                );
            inventoriesWithoutOwner.Add(inventoryDtoWithoutOwner);
        }
        
        GetOwnerObject getOwnerObject = new(Name:owner.Name, Surname:owner.Surname, Email:owner.Email,Inventories:inventoriesWithoutOwner);
        
        
        
        return Results.Ok(getOwnerObject);
    }
    
}
public record AddOwnerRequest(string FirstName, 
    string? LastName, 
    string? Email);

public record GetOwnerObject(string Name, 
    string Surname, 
    string Email, IEnumerable<InventoryDtoWithoutOwner> Inventories);

public record InventoryDtoWithoutOwner(int Id, string Name, string Description, Location Location);