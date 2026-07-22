using BeatCheck.Users.Services.Data.DTOs;
using BeatCheck.Users.Services.Data.Implementations;
using BeatCheck.Users.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeatCheck.Users.WebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userService.LoginAsync(model);

            if (!result.Succeeded)
            {
                return Unauthorized(new { errors = result.Errors });
            }

            return Ok(new { token = result.Token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                return Unauthorized(new { errors = result.Errors });
            }

            return Ok(new { token = result.Token });
        }
    }
}
