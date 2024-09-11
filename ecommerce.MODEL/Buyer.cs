using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Buyer
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long DocumentTypeId { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
