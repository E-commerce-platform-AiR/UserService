using UserService.Database.Entities;

namespace UserService.Models;

public sealed class UserResponse
{
    public Guid Id {get; set;}
    public string Email { get; set; }
    public string Name { get; set; }
    public bool IsAdmin { get; set; }

    public UserResponse(UserEntity userEntity)
    {
        Id = userEntity.Id;
        Name = userEntity.UserName;
        Email = userEntity.Email;
        IsAdmin = userEntity.IsAdmin;
    }

    public UserResponse()
    {
        
    }
}