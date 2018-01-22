using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TaskManagementOsvin.Models
{
    [Serializable]
    [XmlRoot("EmployeeDailyReportModel")]
    public class EmployeeDailyReportModel
    {
        public long DailyReportId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportDateStr { get; set; }
        public long ProjectId { get; set; }
        public int ReportCategoryId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string WorkingHours { get; set; }
        public int TotalNumberCategoryBy { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public string Filename { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public string ProjectTitle { get; set; }
        public string CategoryName { get; set; }
    }
}