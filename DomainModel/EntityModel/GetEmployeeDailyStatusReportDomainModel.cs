using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
   public class GetEmployeeDailyStatusReportDomainModel
    {
        public long DailyReportId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime ReportDate { get; set; }
        public long ProjectId { get; set; }
        public int ReportCategoryId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string WorkingHours { get; set; }
        public int TotalNumberCategoryBy { get; set; }
        public string Filename { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateStr { get; set; }
        public string CreatedBy { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public string ProjectTitle { get; set; }
        public string CategoryName { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string Time { get; set; }

    }
}
