namespace UserService.Models.Exceptions;

public class UserIsNotAdminException : Exception
{
    public UserIsNotAdminException() : base("User with this name or email already exists") { }
    public UserIsNotAdminException(string message) : base(message) { }
}