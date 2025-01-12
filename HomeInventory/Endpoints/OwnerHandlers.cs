using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Endpoints;

public static  class OwnerHandlers
{
    private static async  Task<bool> IsEmailUnique(HomeInventoryDbContext db, string? email)
    {
        if (email is null)
        {
            return false;
        }
        
        return !await db.Owners.AnyAsync(owner=>owner.Email == email);
    }
    public  static async Task<IResult> AddOwner(HomeInventoryDbContext db,AddOwnerRequest request)
    {

        if (!await IsEmailUnique(db, request.Email))
        {
            return Results.BadRequest("");
        }
        
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
            .Include(o => o.Inventories).ThenInclude(inventory => inventory.Location)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (owner is null)
        {
            return Results.NotFound("Owner Not Found");
        }

        List<InventoryDtoWithoutOwner> inventoriesWithoutOwner = new();
        
        foreach (Inventory inventory in owner.Inventories)
        {
            InventoryDtoWithoutOwner inventoryDtoWithoutOwner = new (
                Id:inventory.Id,
                Name:inventory.Name,
                Description:inventory.Description ,
                Location:inventory.Location
                );
            inventoriesWithoutOwner.Add(inventoryDtoWithoutOwner);
        }
        
        GetOwnerObject getOwnerObject = new(Name:owner.Name, Surname:owner.Surname, Email:owner.Email,Inventories:inventoriesWithoutOwner);
        
        
        
        return Results.Ok(getOwnerObject);
    }

    public static async Task<IResult> GetOwners(HomeInventoryDbContext db)
    {
       return  Results.Ok(await db.Owners.ToListAsync());
    }
    
}
public record AddOwnerRequest(string FirstName, 
    string? LastName, 
    string? Email);

public record GetOwnerObject(string Name, 
    string? Surname, 
    string? Email, IEnumerable<InventoryDtoWithoutOwner> Inventories);

public record InventoryDtoWithoutOwner(int Id, string Name, string? Description, Location? Location);
