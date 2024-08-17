using Microsoft.AspNetCore.Mvc;
using SonarTrack.Application.Abstractions.UseCases;
using SonarTrack.WebApi.Abstractions;

namespace SonarTrack.WebApi.Controllers
{
    public class AnalysisController : SonarTrackController
    {
        private readonly ITrackerUseCase _trackerUseCase;

        public AnalysisController(ITrackerUseCase trackerUseCase)
        {
            _trackerUseCase = trackerUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _trackerUseCase.TrackAsync();
            return Ok();
        }
    }
}
