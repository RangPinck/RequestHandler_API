using System;
using System.Collections.Generic;

namespace RequestHandler.Models;

public partial class Document
{
    public Guid DocumentId { get; set; }

    public string Title { get; set; } = null!;

    public Guid? Appointment { get; set; }

    public virtual Appointment? AppointmentNavigation { get; set; }
}
