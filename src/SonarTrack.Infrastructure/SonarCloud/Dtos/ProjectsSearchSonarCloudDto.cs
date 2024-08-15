namespace SonarTrack.Infrastructure.SonarCloud.Dtos
{
    internal class ProjectsSearchSonarCloudDto
    {
        public PagingSonarCloudDto Paging { get; set; }
        public List<ComponentSonarCloudDto> Components { get; set; }
    }
}
