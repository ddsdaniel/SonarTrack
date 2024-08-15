using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Application.Abstractions.Infrastructure;
using SonarTrack.Infrastructure.SonarCloud.HttpClients;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class HttpClientsConfig
    {
        public static IServiceCollection AddHttpClientsConfig(this IServiceCollection services)
        {
            services.AddHttpClient<ISonarHttpClient, SonarCloudHttpClient>();
            return services;
        }
    }
}
