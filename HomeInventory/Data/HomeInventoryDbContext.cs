using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Data;

public class HomeInventoryDbContext: DbContext
{
    public HomeInventoryDbContext(DbContextOptions<HomeInventoryDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>()
            .HasDiscriminator<string>("LocationType")
            .HasValue<AdressLocation>("Address")
            .HasValue<CoordinateLocation>("Coordinate");

        modelBuilder.Entity<PossibleValue>()
            .HasDiscriminator<string>("PossibleValueType")
            .HasValue<MonetaryValue>("Monetary")
            .HasValue<OtherValue>("other");

    }

    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Owner> Owners { get; set; }
    
}