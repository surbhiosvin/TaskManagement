using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ProjectDomainModel
    {
        public long ProjectId { get; set; }
        public long ClientId { get; set; }
        public long DepartmentId { get; set; }
        public string TechnologyName { get; set; }
        public long ProjectTypeId { get; set; }
        public string ProjectTitle { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string UploadDetailsDocument { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectStatus { get; set; }
        public string Archived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long CreatedBy { get; set; }
    }
}
