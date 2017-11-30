using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ProjectAssignToUserTotalWorkingHoursDomainModel
    {
        public long EmployeeId { get; set; }
        public long ProjectId { get; set; }
        public long ReportCategoryId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string ProjectTitle { get; set; }
        public string CategoryName { get; set; }
        public decimal EmployeeWorkingHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
    }
}
