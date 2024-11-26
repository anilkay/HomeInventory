using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HomeInventoryDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
   using (var scope = app.Services.CreateScope())
    {
       var dbContext = scope.ServiceProvider.GetRequiredService<HomeInventoryDbContext>();
        dbContext.Database.Migrate(); // Migration'ları otomatik olarak uygular
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello", () => "Hello, World!");

app.MapGet("/inventory", async (HomeInventoryDbContext db) =>
    await db.Inventories.ToListAsync());

// READ: Belirli bir envanter öğesini ID'ye göre getirme
app.MapGet("/inventory/{id}", async (HomeInventoryDbContext db, int id) =>
{
    var inventory = await db.Inventories.FindAsync(id);
    return inventory is not null ? Results.Ok(inventory) : Results.NotFound();
});

app.MapPost("/inventory", async (HomeInventoryDbContext db, Inventory inventory) =>
{
    db.Inventories.Add(inventory);
    await db.SaveChangesAsync();
    return Results.Created($"/inventory/{inventory.Id}", inventory);
});

app.MapPost("/inventorywithaddress",
    async (HomeInventoryDbContext db, InventoryWithAddressDto inventoryWithAddressDto) =>
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
    });

app.MapPost("/inventoryWithCoordinates",
    async (HomeInventoryDbContext db, InventoryWithCoordinatesDto inventoryWithCoordinatesDto) =>
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
);


// UPDATE: Envanter öğesini güncelleme
app.MapPut("/inventory/{id}", async (HomeInventoryDbContext db, int id, Inventory updatedInventory) =>
{
    var inventory = await db.Inventories.FindAsync(id);
    if (inventory is null) return Results.NotFound();

    inventory.Name = updatedInventory.Name;
    inventory.Possible = updatedInventory. Possible;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE: Envanter öğesini silme
app.MapDelete("/inventory/{id}", async (HomeInventoryDbContext db, int id) =>
{
    var inventory = await db.Inventories.FindAsync(id);
    if (inventory is null) return Results.NotFound();

    db.Inventories.Remove(inventory);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/health", async (HomeInventoryDbContext db) =>
{
   await  db.Inventories.ToListAsync();
   return Results.Ok();
} );


app.Run();
public record InventoryDto(string Name, string Description,double Possible);
public record InventoryWithAddressDto(string Name, string Description,double Possible,string LocationName,string Address):InventoryDto(Name,Description,Possible);
public record InventoryWithCoordinatesDto(string Name, string Description, double Possible, string LocationName,double X, double Y):InventoryDto(Name,Description,Possible);
