using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Repositories
{
    public interface IEmployeeDailyStatusReport
    {
        List<ProjectReportCategoryDomainModel> GetProjectReportCategoryByDeptId(long DepartmentId);
        ResponseDomainModel AddUpdateEmployeeDailyReport(AddEmployeeDailyStatusReportDomainModel model);
        List<EmployeeDailyReportDomainModel> GetDailyStatusReportDetailsOfCurrentDate(long employeeid);
        List<EmployeeTotalWorkingHoursReportDomainModel> GetEmployeeTotalWorkingHoursReport(long DepartmentId, long EmployeeId, string StartDate, string EndDate);
        List<GetEmployeeDailyStatusReportDomainModel> GetEmployeeDailyReportDetails(long DepartmentId, long EmployeeId, string ReportDate);
    }
}
