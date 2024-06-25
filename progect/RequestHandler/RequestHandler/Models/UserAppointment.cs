using System;
using System.Collections.Generic;

namespace RequestHandler.Models;

public partial class UserAppointment
{
    public Guid Appointment { get; set; }

    public Guid User { get; set; }

    public virtual Appointment AppointmentNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
