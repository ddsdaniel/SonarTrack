using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Application.Abstractions.UseCases;

namespace SonarTrack.Application.UseCases
{
    public class TrackerUseCase : ITrackerUseCase
    {
        private readonly IMonthlyDataPurgeService _monthlyDataPurgeService;
        private readonly IAnalysisService _analysisService;

        public TrackerUseCase(
            IMonthlyDataPurgeService monthlyDataPurgeService,
            IAnalysisService analysisService
            )
        {
            _monthlyDataPurgeService = monthlyDataPurgeService;
            _analysisService = analysisService;
        }

        public async Task TrackAsync()
        {
            await _monthlyDataPurgeService.PurgeAsync();
            var analises = await _analysisService.GetAnalysesAsync();
        }
    }
}
