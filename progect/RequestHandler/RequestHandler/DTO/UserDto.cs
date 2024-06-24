namespace RequestHandler.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public string Login { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Role { get; set; }
    }
}
