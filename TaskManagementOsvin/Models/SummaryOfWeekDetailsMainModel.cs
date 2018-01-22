using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class SummaryOfWeekDetailsMainModel
    {
        public SummaryOfWeekDetailsMainModel()
        {
            subweeksummary = new List<SummaryOfWeekSubDetailsMainModel>();
        }
        public List<SummaryOfWeekSubDetailsMainModel> subweeksummary { get; set; }
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public decimal WorkingHours { get; set; }
        public string ClientName { get; set; }
       
    }

    public class SummaryOfWeekSubDetailsMainModel
    {
        public long ProjectId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal WorkingHours { get; set; }
        //public DepartmentNameModel DepartmentType { get; set; }
    }

    public enum DepartmentNameModel
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