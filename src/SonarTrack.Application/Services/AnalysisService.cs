using Microsoft.Extensions.Logging;
using SonarTrack.Application.Abstractions.Infrastructure;
using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Application.Dtos;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Application.Enums;
using SonarTrack.Application.Extensions;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly ISonarHttpClient _sonarHttpClient;
        private readonly ILogger<AnalysisService> _logger;

        public AnalysisService(ISonarHttpClient sonarHttpClient, ILogger<AnalysisService> logger)
        {
            _sonarHttpClient = sonarHttpClient;
            _logger = logger;
        }

        public async Task<OperationResultDto<IEnumerable<Analysis>>> GetAnalysesAsync()
        {
            _logger.LogInformation("GetProjectsAsync...");

            var projectsResult = await _sonarHttpClient.GetProjectsAsync();

            if (projectsResult.Success)
            {
                var projects = projectsResult.Value ?? [];

                var analises = InitializeAnalises(projects);

                await SetQualityGateAsync(projects, analises);

                return await SetMeasuresAsync(projects, analises);
            }
            else
            {
                return OperationResultDto<IEnumerable<Analysis>>.Fail(projectsResult.Errors);
            }
        }

        private async Task<OperationResultDto<IEnumerable<Analysis>>> SetMeasuresAsync(IEnumerable<ProjectDto>? projects, List<Analysis> analises)
        {
            var allMetricKeys = Enum.GetValues(typeof(MetricKey)).Cast<MetricKey>();

            _logger.LogInformation("GetMeasuresAsync...");

            var measuresResult = await _sonarHttpClient.GetMeasuresAsync(projects, allMetricKeys);

            if (measuresResult.Success)
            {
                var measures = measuresResult.Value ?? [];

                foreach (var measure in measures)
                {
                    var analisys = analises.FirstOrDefault(a => a.ProjectKey == measure.Component);
                    SetMetric(measure, analisys);
                }

                return OperationResultDto<IEnumerable<Analysis>>.Ok(analises);
            }
            else
            {
                return OperationResultDto<IEnumerable<Analysis>>.Fail(measuresResult.Errors);
            }
        }

        private List<Analysis> InitializeAnalises(IEnumerable<ProjectDto> projects)
        {
            var analises = new List<Analysis>();
            foreach (var project in projects)
            {
                var analisys = new Analysis { ProjectKey = project.Key };
                analises.Add(analisys);
            }
            return analises;
        }

        private void SetMetric(MeasureDto measure, Analysis analisys)
        {
            if (measure.Metric.TryGetEnum(out MetricKey metricKey))
            {
                switch (metricKey)
                {
                    case MetricKey.alert_status:
                        break;
                    case MetricKey.bugs:
                        analisys.Bugs = int.Parse(measure.Value);
                        break;
                    case MetricKey.reliability_rating:
                        break;
                    case MetricKey.vulnerabilities:
                        analisys.Vulnerabilities = int.Parse(measure.Value);
                        break;
                    case MetricKey.security_rating:
                        analisys.SecurityRating = measure.Value[0];
                        break;
                    case MetricKey.security_hotspots_reviewed:
                        break;
                    case MetricKey.security_review_rating:
                        break;
                    case MetricKey.code_smells:
                        analisys.CodeSmells = int.Parse(measure.Value);
                        break;
                    case MetricKey.sqale_rating:
                        break;
                    case MetricKey.duplicated_lines_density:
                        analisys.DuplicatedLinesDensity = decimal.Parse(measure.Value);
                        break;
                    case MetricKey.coverage:
                        analisys.Coverage = decimal.Parse(measure.Value);
                        break;
                    case MetricKey.ncloc:
                        analisys.NonCommentingLinesOfCode = int.Parse(measure.Value);
                        break;
                    case MetricKey.ncloc_language_distribution:
                        break;
                    case MetricKey.maintainability:
                        analisys.MaintainabilityRating = measure.Value[0];
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task SetQualityGateAsync(IEnumerable<ProjectDto>? projects, List<Analysis> analises)
        {
            foreach (var project in projects)
            {
                var analisys = analises.FirstOrDefault(a => a.ProjectKey == project.Key);

                _logger.LogInformation("GetQualityGateAsync: {Project}", project.Key);

                var qualityGateResult = await _sonarHttpClient.GetQualityGateAsync(project);

                if (qualityGateResult.Success)
                {
                    analisys.QualityGate = qualityGateResult.Value.Name;
                }
            }
        }
    }
}
