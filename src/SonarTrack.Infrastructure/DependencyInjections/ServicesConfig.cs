using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Application.Services;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class ServicesConfig
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services)
        {
            services.AddScoped<IAnalysisService, AnalysisService>();
            services.AddScoped<IMonthlyDataPurgeService, MonthlyDataPurgeService>();
            return services;
        }
    }
}
