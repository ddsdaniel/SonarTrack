using SonarTrack.Domain.Entities;
using SonarTrack.Infrastructure.Abstractions;

namespace SonarTrack.Infrastructure.Data.Repositories
{
    public class AnalysisRepository : EntityFrameworkRepository<Analysis>
    {
        public AnalysisRepository(SonarTrackDbContext sonarTrackDbContext) : base(sonarTrackDbContext)
        {
        }
    }
}
