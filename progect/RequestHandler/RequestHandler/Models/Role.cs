namespace RequestHandler.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
