using UserService.Database.Entities;

namespace UserService.Database.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserEntity>> GetUserList();
    Task<UserEntity> GetUser(string userName, string password);
    Task<UserEntity> GetUser(Guid userId);
    Task InsertUserAsync(UserEntity user);
    Task SaveAsync();
}