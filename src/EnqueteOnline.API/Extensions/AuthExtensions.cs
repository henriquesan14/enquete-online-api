using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EnqueteOnline.API.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var isDevelopment = env.IsDevelopment();

            var secretKey = Encoding.ASCII.GetBytes(configuration["TokenSettings:Secret"]!);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
            });

            var builder = services.AddAuthorizationBuilder();

            return services;
        }
    }
}
