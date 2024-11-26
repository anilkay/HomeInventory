using HomeInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Configuration;

public static  class AppConfiguration
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<HomeInventoryDbContext>(
            
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
    }
}