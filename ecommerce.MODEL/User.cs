using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class User
{
    public long IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? ImageUrl { get; set; }

    public bool Activated { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual Seller? Seller { get; set; }

    public virtual ICollection<AppAuthority> AuthorityNames { get; set; } = new List<AppAuthority>();
}
