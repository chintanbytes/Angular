namespace MyShop.WebApi.Data;

public partial class Employee : BaseEntity
{
    public string ApplicationUserId { get; set; }

    public long AddressId { get; set; }

    public long PhoneNumberId { get; set; }

    public long? ReportsTo { get; set; }

    public DateTime? HireDate { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public virtual Address Address { get; set; }

    public virtual PhoneNumber PhoneNumber { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }

    public virtual Employee? ReportsToNavigation { get; set; }

    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}