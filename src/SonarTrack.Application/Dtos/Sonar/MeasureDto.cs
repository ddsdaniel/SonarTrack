namespace SonarTrack.Application.Dtos.Sonar
{
    public class MeasureDto
    {
        public string Metric { get; set; }
        public string Value { get; set; }
        public string Component { get; set; }
        public bool? BestValue { get; set; }
    }
}
