namespace RequestHandler.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Appointment> AppointmentApprovalNavigations { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentMasterNavigations { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentUserNavigations { get; set; } = new List<Appointment>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
