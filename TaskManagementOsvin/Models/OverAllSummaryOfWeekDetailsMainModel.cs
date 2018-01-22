using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class OverAllSummaryOfWeekDetailsMainModel
    {
        public decimal TotalWorkingHours { get; set; }
        public decimal TotalDotNetWorkingHours { get; set; }
        public decimal TotalPhpWorkingHours { get; set; }
        public decimal TotalAndroidWorkingHours { get; set; }
        public decimal TotalIOSWorkingHours { get; set; }
        public decimal TotalDesignWorkingHours { get; set; }
        public decimal TotalSeoWorkingHours { get; set; }
        public decimal TotalQaWorkingHours { get; set; }
        public decimal TotalHybridWorkingHours { get; set; }
        public decimal TotalWeeklyWorkingHours { get; set; }
        public decimal OverallTotalWorkingHours { get; set; }
        public List<SummaryOfWeekDetailsMainModel> SummaryOfWeekDetails { get; set; }
    }
}