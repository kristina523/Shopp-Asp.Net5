using System;
using System.Collections.Generic;

namespace Shopp.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? TotalPrice { get; set; }

    public virtual Customer? Customer { get; set; }
}
