using Hangfire.Dashboard;

namespace EnqueteOnline.API.Filters
{
    public class CookieAuthDashboardFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            if (httpContext.User.Identity?.IsAuthenticated != true)
                return false;

            return httpContext.User.HasClaim("Permissions", "HANGFIRE");
        }
    }
}
