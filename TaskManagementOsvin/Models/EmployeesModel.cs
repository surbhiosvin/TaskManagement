using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class EmployeesModel
    {
        public long UserId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
    }
}