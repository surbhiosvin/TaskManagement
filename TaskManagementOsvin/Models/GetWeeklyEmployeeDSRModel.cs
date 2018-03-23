using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class GetWeeklyEmployeeDSRModel
    {
        public long ProjectID { get; set; }
        public long DailyReportId { get; set; }
        public long EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ClientName { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReportDate { get; set; }
        public string WorkingHoursOfProject { get; set; }
        public string TotalWorkingHoursOfProject { get; set; }
        public string Profiles { get; set; }
        public decimal ReleasedAmount { get; set; }
        public string PaymentDueDate { get; set; }
        public string ProjectCost { get; set; }
        public string EstimateHours { get; set; }
        public string EffectiveRate { get; set; }
        public string RateTillDate { get; set; }
    }
}