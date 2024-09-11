using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class DocumentType
{
    public long Id { get; set; }

    public string Acronyms { get; set; } = null!;

    public string DocumentName { get; set; } = null!;

    public virtual ICollection<Buyer> Buyers { get; set; } = new List<Buyer>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
