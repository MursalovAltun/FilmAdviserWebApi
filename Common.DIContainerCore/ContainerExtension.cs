using Common.DataAccess.EFCore;
using Common.DataAccess.EFCore.Repositories;
using Common.Entities;
using Common.Services;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration, string connectionString = null)
        {
            services.AddDbContextPool<DataContext>(options => options
                .UseMySql(connectionString));

            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();

            InitServices(services, configuration);

            InitRepositories(services, configuration);
        }

        private static void InitServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IUserService, UserService<User>>();
        }

        private static void InitRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IIdentityUserRepository<User>, IdentityUserRepository>();
            services.AddTransient<IRoleRepository<Role>, RoleRepository>();
            services.AddTransient<IUserRoleRepository<UserRole>, UserRoleRepository>();
            services.AddTransient<IUserClaimRepository<UserClaim>, UserClaimRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IUserPhotoRepository, UserPhotoRepository>();
        }
    }
}