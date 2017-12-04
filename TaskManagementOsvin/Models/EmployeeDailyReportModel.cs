using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class EmployeeDailyReportModel
    {
        public long DailyReportId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime ReportDate { get; set; }
        [Required(ErrorMessage = "Please select")]
        public long ProjectId { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public int ReportCategoryId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string TaskName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string WorkingHours { get; set; }
        public int TotalNumberCategoryBy { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public string Filename { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}