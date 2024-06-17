using UserService.Database.Entities;

namespace UserService.Services.Interfaces;

public interface IAdminService
{
    Task<bool> DeleteOffer(Guid userId, long offerId);
    Task<bool> DeleteUser(Guid userId);
    Task<UserEntity> SetUserRole(Guid userId, bool isAdmin);
}