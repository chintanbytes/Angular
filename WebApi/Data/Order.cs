namespace MyShop.WebApi.Data;

public partial class Order : BaseEntity
{
    public long? CustomerId { get; set; }

    public long? EmployeeId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public long ShippingAddressId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Address ShippingAddress { get; set; }

    public virtual List<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
}