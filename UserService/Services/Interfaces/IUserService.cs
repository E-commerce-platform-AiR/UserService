using UserService.Database.Entities;
using UserService.Models;

namespace UserService.Services.Interfaces;

public interface IUserService
{
    Task<UserEntity> PostUser(User user);
    Task<UserEntity> GetUser(string userName, string password);
    Task<UserResponse> GetUser(Guid userId);
}