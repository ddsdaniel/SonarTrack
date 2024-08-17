using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Application.Abstractions.Mappers;
using SonarTrack.Application.Mappers;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class MappersConfig
    {
        public static IServiceCollection AddMappersConfig(this IServiceCollection services)
        {
            services.AddScoped<IMeasureToAnalysisMapper, MeasureToAnalysisMapper>();
            return services;
        }
    }
}
