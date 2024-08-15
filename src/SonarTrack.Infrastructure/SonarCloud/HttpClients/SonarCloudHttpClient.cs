using Microsoft.Extensions.Options;
using SonarTrack.Application.Abstractions.Infrastructure;
using SonarTrack.Application.Dtos;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Application.Enums;
using SonarTrack.Infrastructure.Abstractions;

namespace SonarTrack.Infrastructure.SonarCloud.HttpClients
{
    public class SonarCloudHttpClient : RestHttpClient, ISonarHttpClient
    {
        private readonly IOptions<SonarOptions> _sonarOptions;

        public SonarCloudHttpClient(
            HttpClient httpClient,
            IOptions<SonarOptions> sonarOptions
            ) : base(httpClient)
        {
            _sonarOptions = sonarOptions;
            SetAuthorization(sonarOptions.Value.Token);
        }

        public async Task<OperationResultDto<IEnumerable<ProjectDto>>> GetProjectsAsync()
        {
            var route = $"{_sonarOptions.Value.BaseUrl}" +
                $"/projects" +
                $"/search" +
                $"?organization={_sonarOptions.Value.Organization}" +
                $"&ps={_sonarOptions.Value.PageSize}";

            return await GetAsync<IEnumerable<ProjectDto>>(route);
        }

        public async Task<OperationResultDto<QualityGateDto>> GetQualityGateAsync(ProjectDto project)
        {
            var route = $"{_sonarOptions.Value.BaseUrl}" +
                $"/qualitygates" +
                $"/get_by_project" +
                $"?organization={_sonarOptions.Value.Organization}" +
                $"&project={project.Key}";

            return await GetAsync<QualityGateDto>(route);
        }

        public async Task<OperationResultDto<IEnumerable<MeasureDto>>> GetMeasuresAsync(
            IEnumerable<ProjectDto> projects,
            IEnumerable<MetricKey> metricKeys)
        {
            var projectKeysParam = string.Join(',', projects.Select(p => p.Key));

            var metricKeysParam = string.Join(',', metricKeys);

            var route = $"{_sonarOptions.Value.BaseUrl}" +
                $"/measures" +
                $"/search" +
                $"&projectKeys={projectKeysParam}" +
                $"&metricKeys={metricKeysParam}";

            return await GetAsync<IEnumerable<MeasureDto>>(route);
        }
    }
}
