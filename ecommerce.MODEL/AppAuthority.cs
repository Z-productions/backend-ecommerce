using System;
using System.Collections.Generic;

namespace ecommerce.MODEL;

public partial class AppAuthority
{
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
