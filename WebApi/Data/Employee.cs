namespace MyShop.WebApi.Data;

public partial class Employee : BaseEntity
{
    public DateTime? HireDate { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public virtual Address Address { get; set; }

    public virtual PhoneNumber PhoneNumber { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; }

    public virtual Employee? ReportsTo { get; set; }

    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}