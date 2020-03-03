using Common.DataAccess.EFCore;
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

            InitServices(services, configuration);

            InitRepositories(services, configuration);
        }

        private static void InitServices(IServiceCollection services, IConfiguration configuration)
        {

        }

        private static void InitRepositories(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}