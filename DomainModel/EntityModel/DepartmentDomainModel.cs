using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class DepartmentDomainModel:ResponseDomainModel
    {
        public DepartmentDomainModel()
        {
            listDepartments = new List<DepartmentDomainModel>();
        }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<DepartmentDomainModel> listDepartments { get; set; }
    }
}
