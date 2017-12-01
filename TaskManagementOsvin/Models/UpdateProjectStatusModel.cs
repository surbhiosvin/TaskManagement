using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class UpdateProjectStatusModel
    {
        public long ProjectId { get; set; }
        [Required]
        public string status { get; set; }
        public string ProjectUrl { get; set; }
    }
}