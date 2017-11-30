using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ProjectReportCategoryDomainModel
    {
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string DepartmentName { get; set; }
    }
}
