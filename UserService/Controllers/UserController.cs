﻿using Microsoft.AspNetCore.Mvc;
using UserService.Database.Entities;
using UserService.Database.Repositories.Interfaces;
using UserService.Models;
using UserService.Models.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
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
        var users = await _userRepository.GetUserList();
        return Ok(users);
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

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUser([FromBody] int id)
    {
        return null; // TODO
    }
}