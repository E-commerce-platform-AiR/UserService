using Microsoft.AspNetCore.Mvc;
using UserService.Database.Entities;
using UserService.Database.Repositories.Interfaces;
using UserService.Models;
using UserService.Models.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public UserController(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserEntity>>> GetUserList()
    {
        var users = _userRepository.GetUserList();
        return Ok(users);
    }  
    
    [HttpPost("users")]
    public async Task<ActionResult<UserEntity>> PostUser([FromBody] User user)
    {
        try
        {
            return Ok(await _userService.PostUser(user));
        }
        catch(InvalidUserException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("users")]
    public async Task<ActionResult> DeleteUser([FromBody] int id)
    {
        return null; // TODO
    }
}