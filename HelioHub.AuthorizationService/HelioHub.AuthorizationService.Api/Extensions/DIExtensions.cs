using HelioHub.AuthorizationService.Logic;
using HelioHub.AuthorizationService.Logic.Contracts;
using Microsoft.AspNetCore.Identity;

namespace HelioHub.AuthorizationService.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ConfigureBLLDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
