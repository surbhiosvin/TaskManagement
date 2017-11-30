using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class ProjectWorkingHoursBeforePMSModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please Select Project")]
        public long ProjectId { get; set; }
        [Required(ErrorMessage = "Please Enter Working Hours")]
        public string WorkinghHours { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}