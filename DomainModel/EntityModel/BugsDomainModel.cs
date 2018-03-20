using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class BugsDomainModel
    {
        public long BugId { get; set; }
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public string BugSubject { get; set; }
        public string BugDescription { get; set; }
        public string Files { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string EmployeeFeedBack { get; set; }
        public long BugReportedBy { get; set; }
        public string BugReportedBy_ { get; set; }
        public int ReOpenedCount { get; set; }
        public int OtherReOpenedCount { get; set; }
        public DateTime IssueReportedDate { get; set; }
        public DateTime BugUpdatedDate { get; set; }
        public DateTime IssueClosedDate { get; set; }
        public string Mode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectTitle { get; set; }
    }
}
