using UserService.Database.Entities;
using UserService.Models;

namespace UserService.Database.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserEntity>> GetUserList();
    Task<UserEntity> GetUser(string userName, string password);
    Task<UserResponse> GetUser(Guid userId);
    Task InsertUserAsync(UserEntity user);
    Task SaveAsync();
}