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

#region MapEndpoints
app.MapInventoryRoutes();
app.MapOwnerRoutes();
app.MapHealthRoutes();
#endregion



app.Run();
