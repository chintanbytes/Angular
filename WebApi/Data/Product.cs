namespace MyShop.WebApi.Data;

public partial class Product : BaseEntity
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
