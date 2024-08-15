using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SonarTrack.Infrastructure.SonarCloud.Dtos
{
    internal class ProjectsSearchSonarCloudDto
    {
        public PagingSonarCloudDto Paging { get; set; }
        public List<ComponentSonarCloudDto> Components { get; set; }
    }
}
