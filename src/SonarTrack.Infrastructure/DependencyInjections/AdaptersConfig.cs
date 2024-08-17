using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Application.Abstractions.Adapters;
using SonarTrack.Application.Adapters;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class AdaptersConfig
    {
        public static IServiceCollection AddAdaptersConfig(this IServiceCollection services)
        {
            services.AddScoped<IProjectToAnalysisAdapter, ProjectToAnalysisAdapter>();
            return services;
        }
    }
}
