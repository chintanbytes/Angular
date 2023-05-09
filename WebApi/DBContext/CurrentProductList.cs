using System;
using System.Collections.Generic;

namespace MyShop.WebApi.DBContext;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
