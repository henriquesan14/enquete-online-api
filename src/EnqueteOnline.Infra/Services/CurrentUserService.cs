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

        public void SetRefreshTokenCookies(string refreshToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return;

            httpContext.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(7)
            });
        }

        public void RemoveRefreshTokenCookies()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refresh_token", new CookieOptions
            {
                Path = "/",
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });
        }

        public bool IsMobileClient()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null)
                return false;

            var userAgent = context.Request.Headers["User-Agent"].ToString().ToLower();
            var clientType = context.Request.Headers["X-Client-Type"].ToString().ToLower();

            return clientType == "mobile" ||
                   userAgent.Contains("flutter") || userAgent.Contains("okhttp") ||
                   userAgent.Contains("android") || userAgent.Contains("ios");
        }
    }
}
