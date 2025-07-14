using AspNetCoreRateLimit;
using Carter;
using EnqueteOnline.API.ErrorHandling;
using EnqueteOnline.API.Extensions;
using EnqueteOnline.API.Hubs;
using EnqueteOnline.API.Notification;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Infra.ExternalServices;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace EnqueteOnline.API
{
    public static class DependencyInjection
    {
        public static void ConfigureHostUrls(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                builder.WebHost.UseUrls($"http://*:{port}");
            }
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
        {
            services.AddCorsConfig(builder.Environment);
            services.AddCarter();
            services.AddSingleton<Carter.IValidatorLocator>(_ => null!);
            services.AddJsonSerializationConfig();
            services.AddAuthConfig(configuration, builder.Environment);

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddOpenApi();

            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DbConnection")!);

            services.AddHangfireConfig(configuration);

            services.AddRateLimitingConfig(builder.Configuration);

            services.AddScoped<IEnqueteNotificationService, EnqueteNotificationService>();
            services.AddSignalR();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app, IConfiguration configuration)
        {
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIpRateLimiting();

            app.MapHub<EnqueteHub>("/hubs/enquete");

            app.UseHangfireDashboardWithAuth(configuration);
            app.UseRecurringJobs();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
