

using MyShop.WebApi.Data;

namespace MyShop.WebApi.ResourceParameters;

public class CustomerResourceParameters : BaseResourceParameters
{
    public string OrderBy { get; set; } = "Name";
}