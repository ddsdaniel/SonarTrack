using Microsoft.AspNetCore.Mvc;

namespace SonarTrack.WebApi.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class SonarTrackController : Controller
    {
    }
}
