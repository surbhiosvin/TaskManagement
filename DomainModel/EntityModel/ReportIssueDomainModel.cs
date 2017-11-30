using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ReportIssueDomainModel
    {
        public long ReportId { get; set; }
        public long UserId { get; set; }
        public string IssueSubject { get; set; }
        public string IssueDescription { get; set; }
        public string Priority { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string IsRead { get; set; }
        public string SpeedOfSolution { get; set; }
        public string Communication { get; set; }
        public string Quality { get; set; }
        public string Availability { get; set; }
        public string IssueReportDate { get; set; }
        public string IssueClosedDate { get; set; }
        public string IssueStatusUpdatedDate { get; set; }
        public string IssueContentUpdateDate { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Role { get; set; }
        public string EmployeeName { get; set; }
        public long ReadCount { get; set; }
        public long UnReadCount { get; set; }
        public long TotalCount { get; set; }
    }
}
