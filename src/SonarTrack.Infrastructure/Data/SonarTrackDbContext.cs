using Microsoft.EntityFrameworkCore;
using SonarTrack.Domain.Entities;
using System.Reflection;

namespace SonarTrack.Infrastructure.Data
{
    public class SonarTrackDbContext : DbContext
    {
        public DbSet<Analysis> Analyses { get; set; }
        
        public SonarTrackDbContext(DbContextOptions<SonarTrackDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }
    }
}
