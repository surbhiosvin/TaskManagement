using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class AllProjectsDomainModel
    {
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string TypeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectUrl { get; set; }
        public int TotalRecords { get; set; }
    }
}
