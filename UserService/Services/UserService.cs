using UserService.Database.Entities;
using UserService.Database.Repositories.Interfaces;
using UserService.Models;
using UserService.Models.Exceptions;
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

    public async Task<UserResponse> GetUser(Guid userId)
    {
        return await _userRepository.GetUser(userId);
    }
    public async Task DeleteUser(Guid userId)
    {
        var user = await _userRepository.GetUserEntity(userId);
        if (user == null)
        {
            throw new UserNotFoundException("User not found.");
        }
        
        _userRepository.DeleteUser(userId);
        await _userRepository.SaveAsync();
    }
    public async Task<UserEntity> SetUserRole(Guid userId, bool isAdmin)
    {
        var user = await _userRepository.GetUserEntity(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        user.IsAdmin = isAdmin;
        await _userRepository.SaveAsync();

        return user;
    }
}