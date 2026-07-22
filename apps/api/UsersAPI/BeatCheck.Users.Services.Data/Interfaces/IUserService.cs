using BeatCheck.Users.Services.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatCheck.Users.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<AuthResult> LoginAsync(LoginDto model);
        Task<AuthResult> RegisterAsync(RegisterDto model);
        string GenerateAsymmetricJwt(string userId, string username, string email);

    }
}
