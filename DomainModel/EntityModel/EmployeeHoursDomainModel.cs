using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class EmployeeHoursDomainModel
    {
        public long EmployeeId { get; set; }
        public string WorkingHours { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
