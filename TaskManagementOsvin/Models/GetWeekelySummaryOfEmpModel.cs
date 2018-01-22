using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class GetWeekelySummaryOfEmpModel
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public string ProjectTitle { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string WorkingHours { get; set; }
    }
}