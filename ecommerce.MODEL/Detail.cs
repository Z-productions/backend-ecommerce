using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Detail
{
    public long Id { get; set; }

    public int Quantity { get; set; }

    public float Precie { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
