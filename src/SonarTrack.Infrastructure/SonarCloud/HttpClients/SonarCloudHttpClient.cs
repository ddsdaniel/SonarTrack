﻿using AutoMapper;
using Microsoft.Extensions.Options;
using SonarTrack.Application.Abstractions.Infrastructure;
using SonarTrack.Application.Dtos;
using SonarTrack.Application.Dtos.Sonar;
using SonarTrack.Application.Enums;
using SonarTrack.Infrastructure.Abstractions;
using SonarTrack.Infrastructure.Extensions;
using SonarTrack.Infrastructure.SonarCloud.Dtos;

namespace SonarTrack.Infrastructure.SonarCloud.HttpClients
{
    public class SonarCloudHttpClient : RestHttpClient, ISonarHttpClient
    {
        private readonly IOptions<SonarOptions> _sonarOptions;
        private readonly IMapper _mapper;

        public SonarCloudHttpClient(
            HttpClient httpClient,
            IOptions<SonarOptions> sonarOptions,
            IMapper mapper
            ) : base(httpClient)
        {
            _sonarOptions = sonarOptions;
            _mapper = mapper;
            SetAuthorization(sonarOptions.Value.Token);
        }

        public async Task<OperationResultDto<IEnumerable<ProjectDto>>> GetProjectsAsync()
        {
            var route = $"{_sonarOptions.Value.BaseUrl}" +
                $"/projects" +
                $"/search" +
                $"?organization={_sonarOptions.Value.Organization}" +
                $"&ps={_sonarOptions.Value.PageSize}";

            var sonarResult = await GetAsync<ProjectsSearchSonarCloudDto>(route);

            if (sonarResult.Success)
            {
                var projects = _mapper.Map<IEnumerable<ProjectDto>>(sonarResult.Value.Components);
                return OperationResultDto<IEnumerable<ProjectDto>>.Ok(projects);
            }
            else
            {
                return OperationResultDto<IEnumerable<ProjectDto>>.Fail(sonarResult.Errors);
            }
        }

        public async Task<OperationResultDto<QualityGateDto>> GetQualityGateAsync(ProjectDto project)
        {
            var route = $"{_sonarOptions.Value.BaseUrl}" +
                $"/qualitygates" +
                $"/get_by_project" +
                $"?organization={_sonarOptions.Value.Organization}" +
                $"&project={project.Key}";

            var sonarResult = await GetAsync<QualityGateGetByProjectSonarCloudDto>(route);

            if (sonarResult.Success)
            {
                var qualityGate = _mapper.Map<QualityGateDto>(sonarResult.Value.QualityGate);
                return OperationResultDto<QualityGateDto>.Ok(qualityGate);
            }
            else
            {
                return OperationResultDto<QualityGateDto>.Fail(sonarResult.Errors);
            }
        }

        public async Task<OperationResultDto<IEnumerable<MeasureDto>>> GetMeasuresAsync(
            IEnumerable<ProjectDto> projects,
            IEnumerable<MetricKey> metricKeys)
        {
            const int MAX_PROJECTS = 100;

            var allMeasures = new List<MeasureDto>();

            var metricKeysParam = string.Join(',', metricKeys);

            foreach (var batch in projects.Batch(MAX_PROJECTS))
            {
                var projectKeysParam = string.Join(',', batch.Select(p => p.Key));

                var route = $"{_sonarOptions.Value.BaseUrl}" +
                    $"/measures" +
                    $"/search" +
                    $"?projectKeys={projectKeysParam}" +
                    $"&metricKeys={metricKeysParam}";

                var result = await GetAsync<MeasureSearchSonarCloudDto>(route);
                if (result.Success)
                {
                    var measures = _mapper.Map<IEnumerable<MeasureDto>>(result.Value.Measures);
                    allMeasures.AddRange(measures);
                }
                else
                    OperationResultDto<IEnumerable<MeasureDto>>.Fail(result.Errors);
            }

            return OperationResultDto<IEnumerable<MeasureDto>>.Ok(allMeasures);
        }
    }
}
