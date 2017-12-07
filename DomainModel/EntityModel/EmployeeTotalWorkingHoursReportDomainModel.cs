using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class EmployeeTotalWorkingHoursReportDomainModel
    {
        public EmployeeTotalWorkingHoursReportDomainModel()
        {
            listEmployeeDailyReportDetails = new List<GetEmployeeDailyStatusReportDomainModel>();
        }
        public string CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string TotalWorking { get; set; }
        public List<GetEmployeeDailyStatusReportDomainModel> listEmployeeDailyReportDetails { get; set; }
    }
}
