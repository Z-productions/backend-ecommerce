using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class Seller
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long DocumentTypeId { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public string Bank { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Remuneration> Remunerations { get; set; } = new List<Remuneration>();

    public virtual User User { get; set; } = null!;
}
