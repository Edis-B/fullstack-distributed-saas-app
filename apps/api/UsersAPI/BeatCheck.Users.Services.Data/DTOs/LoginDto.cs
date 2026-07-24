using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeatCheck.Users.Services.Data.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username or Email is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username or Email must be between 3 and 20 characters")]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false;
    }
}
