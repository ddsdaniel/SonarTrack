using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Application.Abstractions.UseCases;
using SonarTrack.Application.UseCases;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class UseCasesConfig
    {
        public static IServiceCollection AddUseCasesConfig(this IServiceCollection services)
        {
            services.AddScoped<ITrackerUseCase, TrackerUseCase>();
            return services;
        }
    }
}
