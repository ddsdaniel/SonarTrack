using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddSingleton((provider) =>
            {
                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(provider, type));

                    cfg.AddMaps("SonarTrack.Infrastructure", "SonarTrack.Application");
                });

                return mapperConfiguration.CreateMapper();
            });

            return services;
        }
    }
}
