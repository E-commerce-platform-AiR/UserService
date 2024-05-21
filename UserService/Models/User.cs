namespace UserService.Models;

public class User
{
    public User() { }
    public User(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}