using Microsoft.Extensions.Logging;
using SonarTrack.Application.Abstractions.Services;
using SonarTrack.Domain.Abstractions.Infrastructure.Data;

namespace SonarTrack.Application.Services
{
    public class MonthlyDataPurgeService(IUnitOfWork unitOfWork, ILogger<MonthlyDataPurgeService> logger) : IMonthlyDataPurgeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<MonthlyDataPurgeService> _logger = logger;

        public async Task PurgeAsync()
        {
            _logger.LogInformation("MonthlyDataPurgeService.PurgeAsync...");

            var thisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0, DateTimeKind.Local);
            var recordsToRemove = _unitOfWork.Analyses.Get().Where(a => a.AnalysisDate == thisMonth);
            await _unitOfWork.Analyses.RemoveAsync(recordsToRemove);
        }
    }
}
