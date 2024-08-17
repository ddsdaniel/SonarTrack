using Microsoft.Extensions.Logging;
using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Application.Abstractions.UseCases;

namespace SonarTrack.Application.UseCases
{
    public class TrackerUseCase : ITrackerUseCase
    {
        private readonly IMonthlyDataPurgeService _monthlyDataPurgeService;
        private readonly IAnalysisService _analysisService;
        private readonly ILogger<TrackerUseCase> _logger;

        public TrackerUseCase(
            IMonthlyDataPurgeService monthlyDataPurgeService,
            IAnalysisService analysisService,
            ILogger<TrackerUseCase> logger
            )
        {
            _monthlyDataPurgeService = monthlyDataPurgeService;
            _analysisService = analysisService;
            _logger = logger;
        }

        public async Task TrackAsync()
        {
            await _monthlyDataPurgeService.PurgeAsync();
            var analyses = await _analysisService.GetAnalysesAsync();

            _logger.LogInformation("Finish");
        }
    }
}
