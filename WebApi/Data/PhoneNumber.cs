namespace MyShop.WebApi.Data;

public class PhoneNumber : BaseEntity
{
    public string Cell { get; set; }
    public string? Home { get; set; }
}