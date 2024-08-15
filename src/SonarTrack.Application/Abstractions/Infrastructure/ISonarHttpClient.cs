using SonarTrack.Application.Dtos;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Application.Enums;

namespace SonarTrack.Application.Abstractions.Infrastructure
{
    public interface ISonarHttpClient
    {
        Task<OperationResultDto<IEnumerable<ProjectDto>>> GetProjectsAsync();
        Task<OperationResultDto<QualityGateDto>> GetQualityGateAsync(ProjectDto project);
        Task<OperationResultDto<IEnumerable<MeasureDto>>> GetMeasuresAsync(IEnumerable<ProjectDto> projects, IEnumerable<MetricKey> metricKeys);
    }
}