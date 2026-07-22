using BeatCheck.Users.Data.Models;
using BeatCheck.Users.Services.Data.DTOs;
using BeatCheck.Users.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatCheck.Users.Services.Data.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public Task<AuthResult> LoginAsync(LoginDto model)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> RegisterAsync(RegisterDto model)
        {
            
            throw new NotImplementedException();
        }
    }
}
