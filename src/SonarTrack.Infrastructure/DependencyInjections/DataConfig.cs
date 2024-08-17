using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SonarTrack.Domain.Abstractions.Infrastructure.Data;
using SonarTrack.Infrastructure.Data;

namespace SonarTrack.Infrastructure.DependencyInjections
{
    internal static class DataConfig
    {
        public static IServiceCollection AddDataConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SonarTrackDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
