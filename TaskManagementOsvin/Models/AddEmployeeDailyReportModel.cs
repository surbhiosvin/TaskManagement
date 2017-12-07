using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TaskManagementOsvin.Models
{
    
    public class AddEmployeeDailyReportModel
    {
        public AddEmployeeDailyReportModel()
        {
            this.listEmployeeDailyStatusReport = new List<EmployeeDailyReportModel>();
        }
       
        public List<EmployeeDailyReportModel> listEmployeeDailyStatusReport { get; set; }
    }
}