using UserService.Database.Repositories.Interfaces;
using UserService.Database.DbContext;
using UserService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Models.Exceptions;

namespace UserService.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShopDbContext _dbContext;

    public UserRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<UserEntity>> GetUserList()
    {
        return await _dbContext.Users.ToListAsync();
    }
    public async Task<UserEntity> GetUser(Guid userId)
    {
        UserEntity? user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return user;
    }
    public async Task<UserEntity> GetUser(string userName, string password)
    {
        if (userName == null) throw new ArgumentNullException(nameof(userName));
        UserEntity? user = await _dbContext.Users
            .Where(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password)
            .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return user;
    }
    public async Task InsertUserAsync(UserEntity user)
    {
        if (await _dbContext.Users.AnyAsync(x => x.UserName == user.UserName || x.Email == user.Email))
        {
            throw new UserAlreadyExistException();
        }
        
        await _dbContext.Users.AddAsync(user);
    }
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}