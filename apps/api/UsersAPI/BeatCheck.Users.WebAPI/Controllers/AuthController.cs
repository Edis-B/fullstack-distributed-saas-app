using BeatCheck.Users.Services.Data.DTOs;
using BeatCheck.Users.Services.Data.Implementations;
using BeatCheck.Users.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BeatCheck.Users.Web.Infrastructure;
using BeatCheck.Users.Web.Infrastructure.Extensions;
using System.Security.Claims;
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
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userService.LoginAsync(model);

            if (!result.Succeeded)
            {
                return Unauthorized(new { errors = result.Errors });
            }

            Response.AppendAuthCookie(result.Token);

            return Ok();
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                return Unauthorized(new { errors = result.Errors });
            }

            Response.AppendAuthCookie(result.Token);

            return Ok();
        }

        [HttpGet("status")]
        [ProducesResponseType(typeof(UserDataDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Status()
        {
            if (User.Identity == null || !User.Identity!.IsAuthenticated)
            {
                return Unauthorized();
            }

            var email = User.FindFirstValue(ClaimTypes.Email);
            var username = User.FindFirstValue(ClaimTypes.Name);

            return Ok(new UserDataDto { Username = username!, Email = email! });
        }
    }
}
