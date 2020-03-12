using Common.DIContainerCore;
using Common.Entities;
using Common.Services.Infrastructure.Services;
using Common.WebApiCore.Identity;
using Common.WebApiCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApiCore.Setup
{
    public class DependenciesConfig
    {
        public static void ConfigureDependencies(IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentContextProvider, CurrentContextProvider>();

            services.AddSingleton<JwtManager>();

            ContainerExtension.Initialize(services, configuration, connectionString);

            services.AddTransient<IAuthenticationService, AuthenticationService<User>>();
        }
    }
}