using Common.Entities;
using Common.IdentityManagementCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApiCore.Setup
{
    public static class IdentityConfig
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.AddTransient<IdentityErrorDescriber>();

            services.AddTransient<IRoleStore<Role>, RoleStore<Role>>();
            services.AddTransient<IUserStore<User>, UserStore<User, Role, UserRole, UserClaim>>();
            services.AddTransient<UserManager<User>, ApplicationUserManager>();

            var identityBuilder = new IdentityBuilder(typeof(User), typeof(User), services);
            identityBuilder.AddDefaultTokenProviders();
        }
    }
}