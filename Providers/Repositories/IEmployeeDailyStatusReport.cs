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
        ResponseDomainModel AddDailyStatusReportByTeamLeader(EmployeeDailyReportDomainModel model);
        List<EmployeeTotalWorkingHoursReportDomainModel> GetEmployeeReportFilledByTeamLeaderWithTotalWorkingHours(long DepartmentId, long TLEmployeeId);
        List<GetEmployeeDailyStatusReportDomainModel> GetEmployeeReportDetailsFilledByTeamLeader(long DepartmentId, long TLEmployeeId, long EmployeeId, string CreatedDate);
        List<EmployeeTotalWorkingHoursReportDomainModel> GetEmployeeReportFilledByTeamLeaderAccToEmployeeId(long TLEmployeeId, long EmployeeId);
        List<GetEmployeeDailyStatusReportDomainModel> GetEmployeeReportAccordingToEmployeeId(long DepartmentId, long TLEmployeeId, long EmployeeId, string CreatedDate);
        EmployeeDailyReportDomainModel GetDailyStatusReportDetailsById(long DailyReportId);
        List<EmployeeTotalWorkingHoursInMonthDomainModel> GetEmployeeTotalWorkingHours(string StartDate, string EndDate);
    }
}
