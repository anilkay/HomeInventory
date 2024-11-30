using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Endpoints;

public static class InventoryHandlers
{
    public static GetInventoryModel MapInventoryToInventoryModel(Inventory inventory)
    {
        GetInventoryModel getInventoryModel;
        var possibleValue = inventory.PossibleValue;
           
        var isPossibleValueNull = possibleValue == null;
            
        if (!isPossibleValueNull && possibleValue is MonetaryValue monetaryValue )
        { 
            getInventoryModel = new GetInventoryModel(Inventory:inventory,MonetaryValue:monetaryValue, OtherValue:null);
        }
        else if (!isPossibleValueNull && possibleValue is OtherValue otherValue)
        {
            getInventoryModel=new GetInventoryModel(Inventory:inventory,OtherValue:otherValue, MonetaryValue:null);
        }
        else
        {
            getInventoryModel= new GetInventoryModel(Inventory:inventory,MonetaryValue:null,OtherValue:null);

        }

        return getInventoryModel;
    }
    public static async Task<IEnumerable<GetInventoryModel>> GetInventories(HomeInventoryDbContext db)
    {
        var inventories = await db.Inventories
            .Include(i=>i.PossibleValue)
            .ToListAsync();
        
        List<GetInventoryModel> getInventoryModels = new();

        foreach (var inventory in inventories)
        {
            var getInventoryModel = MapInventoryToInventoryModel(inventory);
            
            getInventoryModels.Add(getInventoryModel);
        }
        
        return getInventoryModels;
        
    }

    public static async Task<IResult> GetInventoryById(HomeInventoryDbContext db, int id)
    { 
        var inventory = await db.Inventories.FindAsync(id);

        if (inventory == null)
        {
            return Results.NotFound();
        }

        var getInventoryModel = MapInventoryToInventoryModel(inventory);
        
        return Results.Ok(getInventoryModel);
    }
    
   public static  async Task<IResult>  AddInventoryWithAddressAndMonetaryValue(HomeInventoryDbContext db, InventoryWithAddressWithMonetoryValueDto inventoryWithAddressDto)
    {
        AdressLocation location = new AdressLocation
        {
            Name = inventoryWithAddressDto.LocationName,
            Adress = inventoryWithAddressDto.Address
        };

        MonetaryValue monetaryValue = new MonetaryValue
        {
            Currency = inventoryWithAddressDto.MonetaryValueDto.Currency,
            Value = inventoryWithAddressDto.MonetaryValueDto.Value,
        };
        
        
        Inventory inventory = new()
        {
            Name = inventoryWithAddressDto.Name, 
            Description = inventoryWithAddressDto.Description,
            Location = location,
            PossibleValue = monetaryValue
        };
        
        db.Inventories.Add(inventory);
        await db.SaveChangesAsync();
        return Results.Created($"/inventory/{inventory.Id}", inventory);
    }

    public static async Task<IResult> AddInventoryWithAddressAndOtherValue(HomeInventoryDbContext db,
        InventoryWithAddressWithOtherValueDto inventoryWithAddressDto)
    {
        AdressLocation location = new AdressLocation
        {
            Name = inventoryWithAddressDto.LocationName,
            Adress = inventoryWithAddressDto.Address
        };

        OtherValue otherValue = new OtherValue
        {
            Description = inventoryWithAddressDto.OtherValueDto.Description
        };

        
        Inventory inventory = new()
        {
            Name = inventoryWithAddressDto.Name, 
            Description = inventoryWithAddressDto.Description,
            Location = location,
            PossibleValue = otherValue
        };
        
        db.Inventories.Add(inventory);
        await db.SaveChangesAsync();
        return Results.Created($"/inventory/{inventory.Id}", inventory);

    }
   
    public static async Task<IResult> AddInventoryWithCoordinatesAndMonetaryValue(HomeInventoryDbContext db, InventoryWithCoordinatesWithMonetaryValueDto inventoryWithCoordinatesDto)
    {
        CoordinateLocation coordinateLocation = new()
        {
            Name = inventoryWithCoordinatesDto.LocationName,
            X = inventoryWithCoordinatesDto.X,
            Y = inventoryWithCoordinatesDto.Y
        };


        MonetaryValue monetaryValue = new MonetaryValue
        {
            Currency = inventoryWithCoordinatesDto.MonetaryValueDto.Currency,
            Value = inventoryWithCoordinatesDto.MonetaryValueDto.Value,
        };
        
        Inventory inventory = new()
        {
            Name = inventoryWithCoordinatesDto.Name,
            Description = inventoryWithCoordinatesDto.Description,
            Location = coordinateLocation,
            PossibleValue = monetaryValue
        };
        
        db.Inventories.Add(inventory);
        await db.SaveChangesAsync();
        return Results.Created($"/inventory/{inventory.Id}", inventory);
        
    }

    public static async Task<IResult> AddInventoryWithCoordinatesAndOtherValue(HomeInventoryDbContext db,
        InventoryWithCoordinatesWithOtherValueDto inventoryWithCoordinatesDto)
    {
        CoordinateLocation coordinateLocation = new()
        {
            Name = inventoryWithCoordinatesDto.LocationName,
            X = inventoryWithCoordinatesDto.X,
            Y = inventoryWithCoordinatesDto.Y
        };
        
        OtherValue otherValue = new OtherValue
        {
            Description = inventoryWithCoordinatesDto.OtherValueDto.Description
        };
        
        Inventory inventory = new()
        {
            Name = inventoryWithCoordinatesDto.Name, 
            Description = inventoryWithCoordinatesDto.Description,
            Location = coordinateLocation,
            PossibleValue = otherValue
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
public record InventoryDto(string Name, string Description);
public record MonetaryValueDto(Decimal Value, string Currency);

public record OtherValueDto(string Description);

public record InventoryWithAddressWithMonetoryValueDto(string Name, string Description,string LocationName,
    string Address,MonetaryValueDto MonetaryValueDto):InventoryDto(Name,Description);

public record InventoryWithAddressWithOtherValueDto(string Name, string Description,string LocationName,
    string Address,OtherValueDto OtherValueDto):InventoryDto(Name,Description);

public record InventoryWithCoordinatesWithMonetaryValueDto(
    string Name,
    string Description,
    string LocationName,
    double X,
    double Y,
    MonetaryValueDto MonetaryValueDto
) : InventoryDto(Name, Description);

public record InventoryWithCoordinatesWithOtherValueDto(
    string Name,
    string Description,
    string LocationName,
    double X,
    double Y,
    OtherValueDto OtherValueDto
) : InventoryDto(Name, Description);

public record GetInventoryModel(Inventory Inventory,MonetaryValue? MonetaryValue, OtherValue? OtherValue);
