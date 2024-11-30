using System.ComponentModel.DataAnnotations;

namespace HomeInventory.Data;

public class OtherValue: PossibleValue
{
    [MaxLength(150)]
    public required  string Description { get; set; }
    
}