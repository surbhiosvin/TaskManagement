using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class AddEmployeeDailyStatusReportDomainModel
    {
        public AddEmployeeDailyStatusReportDomainModel()
        {
            listEmployeeDailyStatusReport = new List<EmployeeDailyReportDomainModel>();
        }
        public List<EmployeeDailyReportDomainModel> listEmployeeDailyStatusReport { get; set; }
    }
}
