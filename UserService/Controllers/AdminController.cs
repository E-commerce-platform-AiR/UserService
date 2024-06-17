using Microsoft.AspNetCore.Mvc;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

public class AdminController  : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;
    
    public AdminController(IUserService userService, IAdminService adminService)
    {
        _userService = userService;
        _adminService = adminService;
    }
    
    [HttpDelete("")]
    public async Task<ActionResult> RemoveUser(Guid userId)
    {
        try
        {
            return await _adminService.RemoveUser(userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveOffer(Guid userId, long offerId)
    {
        try
        {
            return await _adminService.RemoveOffer(userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    
}