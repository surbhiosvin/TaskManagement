using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class EmployeesDomainModel
    {
        public long UserId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
    }
}
