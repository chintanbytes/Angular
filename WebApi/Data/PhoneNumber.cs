namespace MyShop.WebApi.Data;

public class PhoneNumber : BaseEntity
{
    public string Cell { get; set; }
    public string? Phone { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual Employee? Employee { get; set; }

}