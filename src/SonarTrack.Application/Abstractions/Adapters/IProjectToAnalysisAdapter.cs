using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.Abstractions.Adapters
{
    public interface IProjectToAnalysisAdapter
    {
        IEnumerable<Analysis> Adapt(IEnumerable<ProjectDto> projects);
        Analysis Adapt(ProjectDto project);
    }
}