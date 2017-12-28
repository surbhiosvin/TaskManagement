using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class GetHrsByDateAndProjModel
    {
        public long Projectid { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
}