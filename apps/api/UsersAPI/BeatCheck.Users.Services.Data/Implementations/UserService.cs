using BeatCheck.Users.Data.Models;
using BeatCheck.Users.Services.Data.DTOs;
using BeatCheck.Users.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BeatCheck.Users.Services.Data.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public string GenerateAsymmetricJwt(string userId, string username, string email)
        {
            var keyPath = _config["JwtSettings:PrivateKeyPath"];
            var privateKeyText = File.ReadAllText(keyPath);

            using RSA rsa = RSA.Create(2048);
            rsa.ImportFromPem(privateKeyText);

            var securityKey = new RsaSecurityKey(rsa);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
                Issuer = "BeatCheck.UsersAPI",
                Audience = "BeatCheck.Frontend"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<AuthResult> LoginAsync(LoginDto model)
        {
            if (string.IsNullOrWhiteSpace(model?.UsernameOrEmail))
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new[] { "Invalid username or password." }
                };
            }

            bool isEmail = new EmailAddressAttribute().IsValid(model.UsernameOrEmail);
            ApplicationUser? user = null;
            if (isEmail)
            {
                user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
            }

            if (user == null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new[] { "Invalid username or password." }
                };
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordCorrect)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new[] { "Invalid username or password." }
                };
            }

            var token = GenerateAsymmetricJwt(user.Id, user.UserName!, user.Email!);

            return new AuthResult
            {
                Succeeded = true,
                Token = token
            };
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var errors = new List<String>();

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                errors.Add("Email is already in use!");
            }

            if (await _userManager.FindByNameAsync(model.Username) != null)
            {
                errors.Add("Username is already in use!");
            }

            if (errors.Count > 0)
            {
                return new AuthResult { Succeeded = false, Errors = errors.ToArray() };
            }

            var identityResult = await _userManager.CreateAsync(user);

            if (identityResult.Succeeded!)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new[] { "Error creating user!" }
                };
            }

            var token = GenerateAsymmetricJwt(user.Id, user.UserName, user.Email!);
            return new AuthResult()
            {
                Succeeded = true,
                Token = token
            };
        }
    }
}
