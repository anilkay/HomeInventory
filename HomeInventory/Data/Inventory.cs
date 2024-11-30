using System.ComponentModel.DataAnnotations;

namespace HomeInventory.Data;

public class Inventory
{
    public int Id { get; set; }
    [MaxLength(150)]
    public required string Name { get; set; }
    [MaxLength(750)]
    public  string? Description { get; set; }
    
    public Location? Location { get; set; }
    public PossibleValue? PossibleValue { get; set; }
}