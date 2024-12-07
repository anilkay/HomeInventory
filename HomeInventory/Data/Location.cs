using System.ComponentModel.DataAnnotations;

namespace HomeInventory.Data;

public class Location
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string? Name { get; set; }
    
}

public class AdressLocation : Location
{
    [MaxLength(500)]
    public required string Adress { get; set; }
}

public class CoordinateLocation : Location
{
    public double X { get; set; }
    public double Y { get; set; }
}