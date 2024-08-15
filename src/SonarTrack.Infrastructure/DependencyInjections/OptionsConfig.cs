using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Infrastructure.SonarCloud;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class OptionsConfig
    {
        public static IServiceCollection AddOptionsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SonarOptions>(configuration.GetSection(nameof(SonarOptions)));
            return services;
        }
    }
}
