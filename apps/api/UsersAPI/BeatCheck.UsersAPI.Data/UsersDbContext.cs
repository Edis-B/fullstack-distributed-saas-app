using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeatCheck.UsersAPI.Data
{
    public class UsersDbContext : IdentityDbContext<IdentityUser>
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
