namespace MyShop.WebApi.Data;

public partial class OrderDetail : BaseEntity
{

    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
