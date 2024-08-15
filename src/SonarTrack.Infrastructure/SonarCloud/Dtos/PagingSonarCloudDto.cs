namespace SonarTrack.Infrastructure.SonarCloud.Dtos
{
    internal class PagingSonarCloudDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
