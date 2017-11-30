using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class AllProjectsModel
    {
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string TypeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectUrl { get; set; }
    }
}