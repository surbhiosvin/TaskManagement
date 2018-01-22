using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class WeekelyEmployeeReportModel
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public string ProjectTitle { get; set; }
        public string DepartmentName { get; set; }
        public decimal WorkingHours { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public decimal TotalWeekelyWorkingHours { get; set; }
    }
}