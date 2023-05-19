namespace MyShop.WebApi.Data;

public class Address : BaseEntity
{
    public string Address1 { get; set; }

    public string? Address2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }
}