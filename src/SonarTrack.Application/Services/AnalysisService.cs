using Microsoft.Extensions.Logging;
using SonarTrack.Application.Abstractions.Adapters;
using SonarTrack.Application.Abstractions.Infrastructure;
using SonarTrack.Application.Abstractions.Mappers;
using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Application.Dtos;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Application.Enums;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.Services
{
    public class AnalysisService(
        ISonarHttpClient sonarHttpClient, 
        ILogger<AnalysisService> logger,
        IMeasureToAnalysisMapper measureToAnalysisMapper,
        IProjectToAnalysisAdapter projectToAnalysisAdapter
        ) : IAnalysisService
    {
        private readonly ISonarHttpClient _sonarHttpClient = sonarHttpClient;
        private readonly ILogger<AnalysisService> _logger = logger;
        private readonly IMeasureToAnalysisMapper _measureToAnalysisMapper = measureToAnalysisMapper;
        private readonly IProjectToAnalysisAdapter _projectToAnalysisAdapter = projectToAnalysisAdapter;

        public async Task<OperationResultDto<IEnumerable<Analysis>>> GetAnalysesAsync()
        {
            _logger.LogInformation("GetProjectsAsync...");

            var projectsResult = await _sonarHttpClient.GetProjectsAsync();

            if (projectsResult.Success)
            {
                var projects = projectsResult.Value ?? [];

                var analyses = _projectToAnalysisAdapter.Adapt(projects);

                await SetQualityGateAsync(projects, analyses);

                return await SetMeasuresAsync(projects, analyses);
            }
            else
            {
                return OperationResultDto<IEnumerable<Analysis>>.Fail(projectsResult.Errors);
            }
        }

        private async Task<OperationResultDto<IEnumerable<Analysis>>> SetMeasuresAsync(
            IEnumerable<ProjectDto> projects, 
            IEnumerable<Analysis> analyses)
        {
            var allMetricKeys = Enum.GetValues(typeof(MetricKey)).Cast<MetricKey>();

            _logger.LogInformation("GetMeasuresAsync...");

            var measuresResult = await _sonarHttpClient.GetMeasuresAsync(projects, allMetricKeys);

            if (measuresResult.Success)
            {
                var measures = measuresResult.Value ?? [];

                _measureToAnalysisMapper.Map(measures, analyses);

                return OperationResultDto<IEnumerable<Analysis>>.Ok(analyses);
            }
            else
            {
                return OperationResultDto<IEnumerable<Analysis>>.Fail(measuresResult.Errors);
            }
        }     

        private async Task SetQualityGateAsync(IEnumerable<ProjectDto> projects, IEnumerable<Analysis> analyses)
        {
            foreach (var project in projects)
            {
                var analysis = analyses.FirstOrDefault(a => a.ProjectKey == project.Key);

                if (analysis is null) continue;

                _logger.LogInformation("GetQualityGateAsync: {Project}", project.Key);

                var qualityGateResult = await _sonarHttpClient.GetQualityGateAsync(project);

                if (qualityGateResult.Success)
                {
                    analysis.QualityGate = qualityGateResult.Value?.Name;
                }
            }
        }
    }
}
