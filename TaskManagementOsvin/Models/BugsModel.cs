using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class BugsModel
    {
        public BugsModel()
        {
            listEmployees = new List<EmployeesDomainModel>();
            listProjects = new List<ProjectDomainModel>();
        }
        public long BugId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public long UserId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public long ProjectId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string BugSubject { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string BugDescription { get; set; }
        public string Files { get; set; }
        public HttpPostedFileBase[] PostedFiles { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Priority { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Status { get; set; }
        public string EmployeeFeedBack { get; set; }
        public long BugReportedBy { get; set; }
        public string BugReportedBy_ { get; set; }
        public int ReOpenedCount { get; set; }
        public int OtherReOpenedCount { get; set; }
        public DateTime IssueReportedDate { get; set; }
        public DateTime BugUpdatedDate { get; set; }
        public DateTime IssueClosedDate { get; set; }
        public List<EmployeesDomainModel> listEmployees { get; set; }
        public List<ProjectDomainModel> listProjects { get; set; }
        public string Mode { get; set; }
    }
}