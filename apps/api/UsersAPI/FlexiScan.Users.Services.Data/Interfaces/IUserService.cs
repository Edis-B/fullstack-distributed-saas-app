using FlexiScan.Users.Services.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FlexiScan.Users.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<AuthResult> LoginAsync(LoginDto model);
        Task<AuthResult> RegisterAsync(RegisterDto model);
        Task<UserDataDto> GetUserAsync(ClaimsPrincipal user);
        string GenerateAsymmetricJwt(string userId, string username, string email);

    }
}
