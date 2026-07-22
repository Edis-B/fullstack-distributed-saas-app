using System;
using System.Collections.Generic;
using System.Text;

namespace BeatCheck.Users.Services.Data.DTOs
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
    }
}
