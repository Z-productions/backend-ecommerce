using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Payment
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public float? Amount { get; set; }

    public DateTime? DatePayment { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
