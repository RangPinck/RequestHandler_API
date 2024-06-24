namespace RequestHandler.Models;

public partial class Document
{
    public Guid DocumentId { get; set; }

    public string Title { get; set; } = null!;

    public string Path { get; set; } = null!;
}
