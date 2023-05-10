namespace MyShop.WebApi.ResourceParameters;

public class ProductResourceParameters : BaseResourceParameters
{
    public string? SearchQuery { get; set; }
    public string? CategoryId { get; set; }
}