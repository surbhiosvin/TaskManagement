using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class EmployeeTotalWorkingHoursInMonthDomainModel
    {
        public EmployeeTotalWorkingHoursInMonthDomainModel()
        {
            listEmployeeHours = new List<EmployeeHoursDomainModel>();
        }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string Hours { get; set; }
        public List<EmployeeHoursDomainModel> listEmployeeHours { get; set; }
    }    
}
