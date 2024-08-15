namespace SonarTrack.Infrastructure.SonarCloud.Dtos
{
    internal class ComponentSonarCloudDto
    {
        public string Organization { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Qualifier { get; set; }
        public string Visibility { get; set; }
        public DateTime? LastAnalysisDate { get; set; }
        public string Revision { get; set; }
    }
}
