using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class DesignationModel
    {
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public string DesignationName { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}