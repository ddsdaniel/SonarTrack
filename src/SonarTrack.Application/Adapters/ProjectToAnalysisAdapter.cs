using SonarTrack.Application.Abstractions.Adapters;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Domain.Entities;
using SonarTrack.Domain.Extensions;

namespace SonarTrack.Application.Adapters
{
    public class ProjectToAnalysisAdapter : IProjectToAnalysisAdapter
    {
        public Analysis Adapt(ProjectDto project)
        {
            return new Analysis
            {
                ProjectKey = project.Key
            };
        }

        public IEnumerable<Analysis> Adapt(IEnumerable<ProjectDto> projects)
        {
            var analyses = new List<Analysis>();

            foreach (var project in projects)
            {
                var analysis = Adapt(project);
                analyses.Add(analysis);
            }

            return analyses;
        }
    }
}
