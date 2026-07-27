using Microsoft.AspNetCore.Http;

namespace FlexiScan.Users.Web.Infrastructure.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AppendAuthCookie(this HttpResponse response, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            response.Cookies.Append("jwt_token", token, cookieOptions);
        }
    }
}
