using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Order
{
    public long Id { get; set; }

    public long BuyerId { get; set; }

    public DateOnly? DateBuyer { get; set; }

    public bool? State { get; set; }

    public virtual Buyer Buyer { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual Payment? Payment { get; set; }

    public virtual Remuneration? Remuneration { get; set; }
}
