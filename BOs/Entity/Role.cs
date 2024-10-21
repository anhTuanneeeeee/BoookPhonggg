using System;
using System.Collections.Generic;

namespace BOs.Entity;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Guest> Guests { get; } = new List<Guest>();
}
