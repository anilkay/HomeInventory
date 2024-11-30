using HomeInventory.Configuration;
using HomeInventory.Data;
using HomeInventory.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
AppConfiguration.AddServices(builder.Services,builder.Configuration);

var app = builder.Build();

DbMigrator.AutoMigrate(app,builder.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/inventory", InventoryHandlers.GetInventories);

// READ: Belirli bir envanter öğesini ID'ye göre getirme
app.MapGet("/inventory/{id}", InventoryHandlers.GetInventoryById);



app.MapPost("/inventorywithaddressWithMonetaryValue",InventoryHandlers.AddInventoryWithAddressAndMonetaryValue);
app.MapPost("/inventorywithaddressWithOtherValue",InventoryHandlers.AddInventoryWithAddressAndOtherValue);
   

app.MapPost("/inventoryWithCoordinatesWithMonetaryValue",InventoryHandlers.AddInventoryWithCoordinatesAndMonetaryValue);
app.MapPost("/inventoryWithCoordinatesWithOtherValue",InventoryHandlers.AddInventoryWithCoordinatesAndOtherValue);


// UPDATE: Envanter öğesini güncelleme
app.MapPut("/inventory/{id}", InventoryHandlers.UpdateInventory);

// DELETE: Envanter öğesini silme
app.MapDelete("/inventory/{id}",InventoryHandlers.DeleteInventory);

app.MapGet("/health", async (HomeInventoryDbContext db) =>
{
   await  db.Inventories.ToListAsync();
   return Results.Ok();
} );


app.Run();
