using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface IManagement
    {
        //List<UserDetailsDomainModel> UserListBySearch();
        ResponseDomainModel AddUpdateEmployee(EmployeeDomainModel model);
        List<DepartmentDomainModel> GetDepartments();
        List<DesignationDomainModel> GetDesignationsByDepartmentId(long DepartmentId);
        UserListDomainModel UserListBySearch(long UserId, string Role, string Archived);
        EmployeeDomainModel GetEmployeeDataById(long UserId);
        ResponseDomainModel DeleteDepartmentById(long DepartmentId);
        ResponseDomainModel ActivateDeactivateDepartment(long DepartmentId, bool IsActive);
        ResponseDomainModel AddUpdateDepartment(DepartmentDomainModel model);
        ResponseDomainModel ActivateDeactivateUser(long UserId, bool IsActive);
        ResponseDomainModel DeleteUserById(long UserId);
        ResponseDomainModel UpdateEmployeeArchive(long UserId);
    }
}
