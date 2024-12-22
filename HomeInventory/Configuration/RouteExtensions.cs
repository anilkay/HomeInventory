using HomeInventory.Endpoints;

namespace HomeInventory.Configuration;

public static  class RouteExtensions
{
    public static IEndpointRouteBuilder MapInventoryRoutes(this IEndpointRouteBuilder app)
    {
         var inventoryGroup=app.MapGroup("/Inventory");
         
         inventoryGroup.MapGet("/", InventoryHandlers.GetInventories);

         inventoryGroup.MapGet("/{id}", InventoryHandlers.GetInventoryById);
        
         inventoryGroup.MapPost("/WithAddressWithMonetaryValue",InventoryHandlers.AddInventoryWithAddressAndMonetaryValue);
         inventoryGroup.MapPost("/WithAddressWithOtherValue",InventoryHandlers.AddInventoryWithAddressAndOtherValue);
        
         inventoryGroup.MapPost("/WithCoordinatesWithMonetaryValue",InventoryHandlers.AddInventoryWithCoordinatesAndMonetaryValue);
         inventoryGroup.MapPost("/WithCoordinatesWithOtherValue",InventoryHandlers.AddInventoryWithCoordinatesAndOtherValue);

         inventoryGroup.MapPut("/AddOwner", InventoryHandlers.AddOwnerToInventory);
        
         inventoryGroup.MapPut("/{id}", InventoryHandlers.UpdateInventory);

         inventoryGroup.MapDelete("/{id}",InventoryHandlers.DeleteInventory);
        
        return app;
    }

    public static IEndpointRouteBuilder MapOwnerRoutes(this IEndpointRouteBuilder app)
    {
        var ownerGroup=app.MapGroup("/Owner");
        
        ownerGroup.MapGet("/{id}", OwnerHandlers.GetOwnerWithId);
        ownerGroup.MapPost("/",OwnerHandlers.AddOwner);
        ownerGroup.MapGet("/", OwnerHandlers.GetOwners);
        
        return app;
    }

    public static IEndpointRouteBuilder MapHealthRoutes(this IEndpointRouteBuilder app)
    {
        var healthGroup=app.MapGroup("/Health");
        healthGroup.MapGet("/", HealthHandlers.Health);
        return app;
    }
    
}