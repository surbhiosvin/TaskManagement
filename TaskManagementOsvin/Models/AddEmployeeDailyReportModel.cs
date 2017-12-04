using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class AddEmployeeDailyReportModel
    {
        public AddEmployeeDailyReportModel()
        {
            listEmployeeDailyStatusReport = new List<EmployeeDailyReportModel>();
        }
        public List<EmployeeDailyReportModel> listEmployeeDailyStatusReport { get; set; }
    }
}