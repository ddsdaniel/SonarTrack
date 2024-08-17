using Microsoft.EntityFrameworkCore;
using SonarTrack.Domain.Entities;
using System.Reflection;

namespace SonarTrack.Infrastructure.Data
{
    public class SonarTrackDbContext(DbContextOptions<SonarTrackDbContext> options) : DbContext(options)
    {
        public DbSet<Analysis> Analyses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }
    }
}
