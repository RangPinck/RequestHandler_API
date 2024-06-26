namespace RequestHandler.DTO
{
    public class AppointmentGetDto
    {
        public Guid Appointment { get; set; }

        public Guid User { get; set; }

        public string UserName { get; set; } = null!;

        public string Problem { get; set; } = null!;

        public string? DiscriptionProblem { get; set; }

        public string Place { get; set; } = null!;

        public DateTime DateCreate { get; set; }

        public DateTime? DateApprove { get; set; }

        public DateTime? DateFix { get; set; }

        public string Status { get; set; } = null!;
    }
}
