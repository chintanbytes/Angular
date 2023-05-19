namespace MyShop.WebApi.Data;

public partial class Customer : BaseEntity
{
    public virtual Address Address { get; set; }

    public virtual PhoneNumber PhoneNumber { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}