using System.ComponentModel.DataAnnotations;

namespace MyShop.WebApi.Models;

public class ProductDto : BaseDto
{
    [Required]
    public string ProductName { get; set; }

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

    public bool Discontinued { get; set; }
}
