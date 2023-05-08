using System.ComponentModel.DataAnnotations;

namespace Angular.Models;

public class ProductDto
{
    [Required]
    public string ProductName { get; set; } = null!;

    [Required]
    public int? SupplierId { get; set; }

    [Required]
    public int? CategoryId { get; set; }

    public string? QuantityPerUnit { get; set; }

    [Required]
    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }
}
