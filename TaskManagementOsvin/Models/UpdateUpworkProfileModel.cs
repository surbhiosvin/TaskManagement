using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class UpdateUpworkProfileModel
    {
        public long ProfileId { get; set; }
        public string ProfileName { get; set; }
        public long ProjectId { get; set; }
        public int ProjectTypeId { get; set; }
        public decimal? hourlyRate { get; set; }
        public decimal loggedHours { get; set; }
        public decimal amount { get; set; }
        public long createdBy { get; set; }
    }
}