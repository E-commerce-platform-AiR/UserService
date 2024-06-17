﻿using UserService.ApiReference;
using UserService.Database.Entities;
using UserService.Database.Repositories.Interfaces;
using UserService.Models.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IOfferApiReference _offerApiReference;
    
    public AdminService(IUserRepository userRepository, IOfferApiReference offerApiReference)
    {
        _userRepository = userRepository;
        _offerApiReference = offerApiReference;
    }
    public async Task<bool> DeleteUser(Guid userId)
    {
        var user = await _userRepository.GetUserEntity(userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var userOffersDeletedResponse = await _offerApiReference.DeleteOffersByUserId(userId);
        if (userOffersDeletedResponse)
        {
            await  _userRepository.DeleteUser(userId);
            await _userRepository.SaveAsync();
            return true;
        }

        return false;
    }
    public async Task<UserEntity> SetUserRole(Guid userId, bool isAdmin)
    {
        var userEntity = await _userRepository.GetUserEntity(userId);
        userEntity.IsAdmin = isAdmin;
        await _userRepository.SaveAsync();

        return userEntity;
    }

    public async Task<bool> DeleteOffer(Guid userId, long offerId)
    {
        var userEntity = await _userRepository.GetUser(userId);
        return await _offerApiReference.DeleteOffer(userEntity.Id, offerId);
    }
}