using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class GetDSRModel
    {
        [Required(ErrorMessage = "Please select a date")]
        public string startdate { get; set; }
        [Required(ErrorMessage = "Please select a date")]
        public string enddate { get; set; }
    }
}