using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class SummaryDSRDomainModel
    {
        public SummaryDSRDomainModel()
        {
            dsr = new List<GetWeeklyEmployeeDSRDomainModel>();
        }
        public decimal OverallTotalWorkingHours { get; set; }
        public decimal OverallWeekTotalWorkingHours { get; set; }
        public List<GetWeeklyEmployeeDSRDomainModel> dsr { get; set; }
    }
}
