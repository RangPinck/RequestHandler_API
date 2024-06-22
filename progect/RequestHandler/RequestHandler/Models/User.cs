using System;
using System.Collections.Generic;

namespace RequestHandler.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
