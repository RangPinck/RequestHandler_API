using System;
using System.Collections.Generic;

namespace RequestHandler.Models;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
