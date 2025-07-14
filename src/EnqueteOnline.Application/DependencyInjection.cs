using EnqueteOnline.Application.Behaviors;
using EnqueteOnline.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnqueteOnline.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining<CadastrarUsuarioCommandValidator>();


            return services;
        }
    }
}
