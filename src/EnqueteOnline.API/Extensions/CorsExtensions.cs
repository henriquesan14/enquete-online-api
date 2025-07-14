namespace EnqueteOnline.API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    if (env.IsDevelopment())
                    {
                        builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    }
                    else
                    {
                        builder.WithOrigins("")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    }
                });
            });

            return services;
        }
    }
}
