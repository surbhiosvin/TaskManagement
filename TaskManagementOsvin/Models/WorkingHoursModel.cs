using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class WorkingHoursModel
    {
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public long UserId { get; set; }
        public string WorkingHours { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}