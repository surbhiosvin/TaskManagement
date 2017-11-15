using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class SummaryOfWeekDetailsMainModel
    {
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public decimal WorkingHours { get; set; }
        public string ClientName { get; set; }
    }
}