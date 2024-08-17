using Microsoft.Extensions.Logging;
using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Application.Abstractions.UseCases;
using SonarTrack.Application.Dtos;
using SonarTrack.Domain.Abstractions.Infrastructure.Data;
using SonarTrack.Domain.Entities;

namespace SonarTrack.Application.UseCases
{
    public class TrackerUseCase : ITrackerUseCase
    {
        private readonly IMonthlyDataPurgeService _monthlyDataPurgeService;
        private readonly IAnalysisService _analysisService;
        private readonly ILogger<TrackerUseCase> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TrackerUseCase(
            IMonthlyDataPurgeService monthlyDataPurgeService,
            IAnalysisService analysisService,
            ILogger<TrackerUseCase> logger,
            IUnitOfWork unitOfWork
            )
        {
            _monthlyDataPurgeService = monthlyDataPurgeService;
            _analysisService = analysisService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task TrackAsync()
        {
            await _monthlyDataPurgeService.PurgeAsync();
            var analysesResult = await _analysisService.GetAnalysesAsync();

            if (analysesResult.Success)
            {
                await AddAnalysesAsync(analysesResult);
            }
            else
            {
                _logger.LogError("Error: {Error}", string.Join('\n', analysesResult.Errors));
            }

            _logger.LogInformation("Finish");
        }

        private async Task AddAnalysesAsync(OperationResultDto<IEnumerable<Analysis>> analysesResult)
        {
            var analyses = analysesResult.Value ?? [];

            _logger.LogInformation("Inserting...");
            foreach (var analysis in analyses)
            {
                await _unitOfWork.Analyses.AddAsync(analysis);
            }

            _logger.LogInformation("Saving...");
            await _unitOfWork.SaveAsync();
        }
    }
}
