using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Product
{
    public long Id { get; set; }

    public long SellerId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public float Price { get; set; }

    public int Stock { get; set; }

    public string UrlImage { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual Seller Seller { get; set; } = null!;
}
