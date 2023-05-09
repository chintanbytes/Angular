using System;
using System.Collections.Generic;

namespace MyShop.WebApi.DBContext;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
