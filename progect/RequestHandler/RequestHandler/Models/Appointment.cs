using System;
using System.Collections.Generic;

namespace RequestHandler.Models;

public partial class Appointment
{
    public Guid AppointmentId { get; set; }

    public string Problem { get; set; } = null!;

    public string? DiscriptionProblem { get; set; }

    public string Place { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public DateTime? DateApprove { get; set; }

    public DateTime? DateFix { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual Status StatusNavigation { get; set; } = null!;
}
