namespace RequestHandler.DTO
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }

        public string Title { get; set; } = null!;

        public Guid? Appointment { get; set; }
    }
}
