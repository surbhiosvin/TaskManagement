using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class AddendumModel
    {
        public long TimeEstimationId { get; set; }
        [Required(ErrorMessage = "Please Select Project")]
        public long ProjectId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string EstimateHours { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string AssignHours { get; set; }
        public string Description { get; set; }
        public string UploadDocument { get; set; }
        public HttpPostedFileBase UploadDocumentFile { get; set; }
        public string DeveloperCodingHours { get; set; }
        public string WebServiceHours { get; set; }
        public string DesignHours { get; set; }
        public decimal DesignHourlyRate { get; set; }
        public string QAHours { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public bool IsApprove { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}