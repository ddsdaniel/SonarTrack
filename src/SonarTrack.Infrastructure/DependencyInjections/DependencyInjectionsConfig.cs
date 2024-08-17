using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    public static class DependencyInjectionsConfig
    {
        public static IServiceCollection AddDependencyInjections(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddUseCasesConfig();
            services.AddServicesConfig();
            services.AddHttpClientsConfig();
            services.AddOptionsConfig(configuration);
            services.AddAutoMapperConfig();
            services.AddDataConfig(configuration);
            services.AddAdaptersConfig();
            services.AddMappersConfig();
            return services;
        }
    }
}
