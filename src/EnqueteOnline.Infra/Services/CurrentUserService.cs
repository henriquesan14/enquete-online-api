using EnqueteOnline.Application.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EnqueteOnline.Infra.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return null;
                }
                return Guid.Parse(userId!);
            }
        }

        public string? IpAddress
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                    return null;

                if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
                {
                    return forwardedFor.FirstOrDefault();
                }

                return httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            }
        }

        public string? RefreshToken
        {
            get
            {
                var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"];
                return refreshToken;
            }
        }

        public void SetCookieTokens(string accessToken, string refreshToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return;

            httpContext.Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            httpContext.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        public void RemoveCookiesToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refresh_token", new CookieOptions
            {
                Path = "/",
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });

            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("access_token", new CookieOptions
            {
                Path = "/",
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });
        }
    }
}
