using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class SummaryDSRModel
    {
        public SummaryDSRModel()
        {
            dsr = new List<GetWeeklyEmployeeDSRModel>();
        }
        public decimal OverallTotalWorkingHours { get; set; }
        public decimal OverallWeekTotalWorkingHours { get; set; }
        public List<GetWeeklyEmployeeDSRModel> dsr { get; set; }
    }
}