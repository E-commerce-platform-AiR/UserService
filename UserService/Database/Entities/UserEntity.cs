namespace UserService.Database.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
}