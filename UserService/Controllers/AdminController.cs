using Microsoft.AspNetCore.Mvc;
using UserService.Database.Entities;
using UserService.Models;
using UserService.Models.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("{userId:guid}")]
public class AdminController  : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;
    
    public AdminController(IUserService userService, IAdminService adminService)
    {
        _userService = userService;
        _adminService = adminService;
    }
    
    [HttpGet("/getAllUsers")]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers(Guid userId)
    {
        try
        {
            return Ok(await _adminService.GetAllUsers(userId));
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete("{offerId:long}/deleteOffer")]
    public async Task<ActionResult<bool>> DeleteOffer(Guid userId, long offerId)
    {
        try
        {
            return Ok(await _adminService.DeleteOffer(userId, offerId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    
    [HttpDelete("deleteUser")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        try
        {
            return Ok(await _adminService.DeleteUser(userId));
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPatch("setAdmin")]
    public async Task<ActionResult<UserEntity>> SetUserRoleToAdmin(Guid userId)
    {
        try
        {
            return Ok(await _adminService.SetUserRole(userId, true));
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("removeAdmin")]
    public async Task<ActionResult<UserEntity>> SetUserRoleToUser(Guid userId)
    {
        try
        {
            return Ok(await _adminService.SetUserRole(userId, false));
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
}