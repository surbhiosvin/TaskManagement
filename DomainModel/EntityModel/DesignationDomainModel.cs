using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class DesignationDomainModel:ResponseDomainModel
    {
        public DesignationDomainModel()
        {
            listDesignations = new List<DesignationDomainModel>();
            listDepartments = new List<DepartmentDomainModel>();
        }
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public List<DepartmentDomainModel> listDepartments { get; set; }
        public List<DesignationDomainModel> listDesignations { get; set; }
    }
}
