namespace SonarTrack.Infrastructure.SonarCloud.Dtos
{
    internal class MeasureSonarCloudDto
    {
        public string Metric { get; set; }
        public string Value { get; set; }
        public string Component { get; set; }
        public bool? BestValue { get; set; }
    }
}
