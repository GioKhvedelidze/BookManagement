using BookManagement.DataAccess.Repositories;
using BookManagement.Models.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto request)
    {
        var result = await _userService.RegisterAsync(request.Email, request.Password);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
    {
        var token = await _userService.LoginAsync(request.Email, request.Password);
        return Ok(new { Token = token });
    }
}