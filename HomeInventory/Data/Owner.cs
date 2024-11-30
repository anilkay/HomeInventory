namespace HomeInventory.Data;

public class Owner
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    
    public ICollection<Inventory> Inventories { get; set; }
}