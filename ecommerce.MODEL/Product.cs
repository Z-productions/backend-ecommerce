using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Product
{
    public long Id { get; set; }

    public long SellerId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public float Priece { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual Seller Seller { get; set; } = null!;
}
