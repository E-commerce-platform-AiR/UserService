using Microsoft.AspNetCore.Mvc;
using UserService.Database.Entities;
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
    
    [HttpDelete("{offerId:long}")]
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
    
    [HttpDelete("")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        try
        {
            await _adminService.DeleteUser(userId);
            return NoContent();
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
            var user = await _adminService.SetUserRole(userId, true);
            return Ok(user);
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
            var user = await _adminService.SetUserRole(userId, false);
            return Ok(user);
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