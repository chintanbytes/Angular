using System;
using System.Collections.Generic;

namespace MyShop.WebApi.DBContext;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
