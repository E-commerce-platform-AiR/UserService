using Microsoft.AspNetCore.Mvc;
using UserService.Database.Entities;
using UserService.Models;
using UserService.Models.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserEntity>> PostUser([FromBody] User user)
    {
        try
        {
            return Ok(await _userService.PostUser(user));
        }
        catch (InvalidUserException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserAlreadyExistException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserEntity>> PostUserLogin([FromBody] User user)
    {
        try
        {
            return Ok(await _userService.GetUser(user.Email, user.Password));
        }
        catch (InvalidUserException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid userId)
    {
        try
        {
            return Ok(await _userService.GetUser(userId));
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        try
        {
            await _userService.DeleteUser(userId);
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
    [HttpPatch("{userId:guid}/setAdmin")]
    public async Task<ActionResult<UserEntity>> SetUserRoleToAdmin(Guid userId)
    {
        try
        {
            var user = await _userService.SetUserRole(userId, true);
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

    [HttpPatch("{userId:guid}/removeAdmin")]
    public async Task<ActionResult<UserEntity>> SetUserRoleToUser(Guid userId)
    {
        try
        {
            var user = await _userService.SetUserRole(userId, false);
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