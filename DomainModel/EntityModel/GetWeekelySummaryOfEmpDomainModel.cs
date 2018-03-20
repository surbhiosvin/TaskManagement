using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class GetWeekelySummaryOfEmpDomainModel
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public string ProjectTitle { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string WorkingHours { get; set; }
    }
}
