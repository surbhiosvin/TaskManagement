using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class UpdateProjectStatusDomainModel
    {
        public long ProjectId { get; set; }
        public string status { get; set; }
        public string ProjectUrl { get; set; }
    }
}
