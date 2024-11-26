using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Configuration;

public static class DbMigrator
{
    public static void AutoMigrate(WebApplication app, bool isDevelopment)
    {
        if (!isDevelopment)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HomeInventoryDbContext>();
                dbContext.Database.Migrate(); // Migration'ları otomatik olarak uygular
            }
        }
    }
}