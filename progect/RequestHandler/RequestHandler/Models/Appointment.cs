namespace RequestHandler.Models;

public partial class Appointment
{
    public Guid AppintmentId { get; set; }

    public string Problem { get; set; } = null!;

    public string? DiscriptionProblem { get; set; }

    public string Place { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public DateTime? DateApprove { get; set; }

    public DateTime? DateFix { get; set; }

    public string? Document { get; set; }

    public int Status { get; set; }

    public Guid User { get; set; }

    public Guid? Approval { get; set; }

    public Guid? Master { get; set; }

    public virtual User? ApprovalNavigation { get; set; }

    public virtual User? MasterNavigation { get; set; }

    public virtual Status StatusNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
