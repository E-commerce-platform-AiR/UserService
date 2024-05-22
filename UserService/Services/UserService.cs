using UserService.Database.Entities;
using UserService.Database.Repositories.Interfaces;
using UserService.Models;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserEntity> PostUser(User user)
    {
        UserEntity newUser = new() { UserName = user.UserName, Password = user.Password, Email = user.Email};
        await _userRepository.InsertUserAsync(newUser);
        await _userRepository.SaveAsync();

        return newUser;
    }
    public async Task<UserEntity> GetUser(string userEmail, string password)
    {
        return await _userRepository.GetUser(userEmail, password);
    }

    public async Task<UserEntity> GetUser(Guid userId)
    {
        return await _userRepository.GetUser(userId);
    }
}