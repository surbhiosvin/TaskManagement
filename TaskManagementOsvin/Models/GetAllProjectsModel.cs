using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class GetAllProjectsModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string projecttitle { get; set; }
        public string projecttype { get; set; } = "NonArchive";
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string projectstatusMode { get; set; } = "Open";
    }
}