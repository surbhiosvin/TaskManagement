using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class UserListDomainModel:ResponseDomainModel
    {
        public UserListDomainModel()
        {
            listUsers = new List<EmployeeDomainModel>();
        }
        public List<EmployeeDomainModel> listUsers { get; set; }
    }
}
