using System.ComponentModel.DataAnnotations;

namespace HomeInventory.Data;

public class Owner
{
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(100)]
    public string? Surname { get; set; }
    [MaxLength(150)]
    public string? Email { get; set; }
    
    public ICollection<Inventory> Inventories { get; set; }
}