using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class SummaryOfWeekDetailsMainDomainModel
    {
        public SummaryOfWeekDetailsMainDomainModel()
        {
            subweeksummary = new List<SummaryOfWeekSubDetailsMainDomainModel>();
        }
        public List<SummaryOfWeekSubDetailsMainDomainModel> subweeksummary { get; set; }
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public decimal WorkingHours { get; set; }
        public string ClientName { get; set; }
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
    }

    public class SummaryOfWeekSubDetailsMainDomainModel
    {
        public long ProjectId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal WorkingHours { get; set; }
        public DepartmentNameDomainModel DepartmentType { get; set; }
    }

    public enum DepartmentNameDomainModel
    {
        DotNet = 1,
        Php = 2,
        Android = 3,
        IPhone = 4,
        Design = 5,
        SEO = 6,
        QA = 7,
        Network = 8,
        HybridApp = 9
    }
}
