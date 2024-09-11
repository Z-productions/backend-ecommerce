using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Remuneration
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long SellerId { get; set; }

    public float Balance { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
