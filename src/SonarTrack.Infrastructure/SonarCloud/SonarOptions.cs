namespace SonarTrack.Infrastructure.SonarCloud
{
    public class SonarOptions
    {
        public string BaseUrl { get; set; }
        public string Organization { get; set; }
        public int PageSize { get; set; }
        public string Token { get; set; }
    }
}
