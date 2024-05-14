using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Database.Entities;
using UserService.Models;
using UserService.Models.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

public class AuthorizationController : Controller
{
    private readonly IUserService _userService;
    
    public AuthorizationController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("MainPage")]
    public IActionResult MainPage()
    {
        try
        {
            return View("MainPage");
        }
        catch (Exception)
        {
            return RedirectToAction("Error");
        }
    }
    
    [HttpGet("Error")]
    public IActionResult Error()
    {
        return View("ErrorPage");    
    }
    
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            return RedirectToAction("Error");
        }
        try
        {
            UserEntity user = await _userService.GetUser(userName, password);
            return RedirectToAction("MainPage");
        }
        catch (Exception)
        {
            return RedirectToAction("Error");
        }
    }
    
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(string userName, string password, string email)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Error");
        }
        try
        {
            await _userService.PostUser(new User(userName, password, email));
            //UserEntity user = await _userService.GetUser(userName, password);
            return RedirectToAction("MainPage");
        }
        catch (UserAlreadyExistException)
        {
            return RedirectToAction("Error");
        }
    }
    
}