using System.ComponentModel.DataAnnotations;

namespace HomeInventory.Data;

public class MonetaryValue: PossibleValue
{
    public decimal Value { get; set; }
    [MaxLength(30)]
    public required string Currency { get; set; }
}