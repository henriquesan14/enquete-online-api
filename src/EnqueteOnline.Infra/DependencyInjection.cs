using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Infra.Data;
using EnqueteOnline.Infra.Data.Interceptors;
using EnqueteOnline.Infra.Data.Repositories;
using EnqueteOnline.Infra.ExternalServices;
using EnqueteOnline.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace EnqueteOnline.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");


            services.AddRefitClient<IFacebookApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://graph.facebook.com");
                });

            services.AddRefitClient<IGoogleApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://oauth2.googleapis.com");
                });

            services.AddRefitClient<IGoogleUserApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3");
                });

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddDbContext<EnqueteOnlineDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IEnqueteOnlineDbContext, EnqueteOnlineDbContext>();

            //Repositories
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IEnqueteRepository, EnqueteRepository>();
            services.AddScoped<IVotoRepository, VotoRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFacebookAuthService, FacebookAuthService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            return services;
        }
    }
}
