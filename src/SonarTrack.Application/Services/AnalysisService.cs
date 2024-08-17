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

                var analyses = InitializeAnalyses(projects);

                await SetQualityGateAsync(projects, analyses);

                return await SetMeasuresAsync(projects, analyses);
            }
            else
            {
                return OperationResultDto<IEnumerable<Analysis>>.Fail(projectsResult.Errors);
            }
        }

        private async Task<OperationResultDto<IEnumerable<Analysis>>> SetMeasuresAsync(IEnumerable<ProjectDto>? projects, List<Analysis> analyses)
        {
            var allMetricKeys = Enum.GetValues(typeof(MetricKey)).Cast<MetricKey>();

            _logger.LogInformation("GetMeasuresAsync...");

            var measuresResult = await _sonarHttpClient.GetMeasuresAsync(projects, allMetricKeys);

            if (measuresResult.Success)
            {
                var measures = measuresResult.Value ?? [];

                foreach (var measure in measures)
                {
                    var analysis = analyses.FirstOrDefault(a => a.ProjectKey == measure.Component);
                    SetMetric(measure, analysis);
                }

                return OperationResultDto<IEnumerable<Analysis>>.Ok(analyses);
            }
            else
            {
                return OperationResultDto<IEnumerable<Analysis>>.Fail(measuresResult.Errors);
            }
        }

        private List<Analysis> InitializeAnalyses(IEnumerable<ProjectDto> projects)
        {
            var analyses = new List<Analysis>();
            var thisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0, DateTimeKind.Local);
            foreach (var project in projects)
            {
                var analysis = new Analysis { ProjectKey = project.Key, AnalysisDate = thisMonth };
                analyses.Add(analysis);
            }
            return analyses;
        }

        private void SetMetric(MeasureDto measure, Analysis analysis)
        {
            if (measure.Metric.TryGetEnum(out MetricKey metricKey))
            {
                switch (metricKey)
                {
                    case MetricKey.alert_status:
                        break;
                    case MetricKey.bugs:
                        analysis.Bugs = int.Parse(measure.Value);
                        break;
                    case MetricKey.reliability_rating:
                        break;
                    case MetricKey.vulnerabilities:
                        analysis.Vulnerabilities = int.Parse(measure.Value);
                        break;
                    case MetricKey.security_rating:
                        analysis.SecurityRating = measure.Value[0];
                        break;
                    case MetricKey.security_hotspots_reviewed:
                        break;
                    case MetricKey.security_review_rating:
                        break;
                    case MetricKey.code_smells:
                        analysis.CodeSmells = int.Parse(measure.Value);
                        break;
                    case MetricKey.sqale_rating:
                        break;
                    case MetricKey.duplicated_lines_density:
                        analysis.DuplicatedLinesDensity = decimal.Parse(measure.Value);
                        break;
                    case MetricKey.coverage:
                        analysis.Coverage = decimal.Parse(measure.Value);
                        break;
                    case MetricKey.ncloc:
                        analysis.NonCommentingLinesOfCode = int.Parse(measure.Value);
                        break;
                    case MetricKey.ncloc_language_distribution:
                        break;
                    case MetricKey.maintainability:
                        analysis.MaintainabilityRating = measure.Value[0];
                        break;
                    default:
                        break;
                }
            }
        }

        private async Task SetQualityGateAsync(IEnumerable<ProjectDto>? projects, List<Analysis> analyses)
        {
            foreach (var project in projects)
            {
                var analysis = analyses.FirstOrDefault(a => a.ProjectKey == project.Key);

                _logger.LogInformation("GetQualityGateAsync: {Project}", project.Key);

                var qualityGateResult = await _sonarHttpClient.GetQualityGateAsync(project);

                if (qualityGateResult.Success)
                {
                    analysis.QualityGate = qualityGateResult.Value.Name;
                }
            }
        }
    }
}
