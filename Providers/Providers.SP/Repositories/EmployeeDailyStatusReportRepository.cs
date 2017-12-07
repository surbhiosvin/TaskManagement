using DomainModel.EntityModel;
using Providers.Helper;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Providers.SP.Repositories
{
    public class EmployeeDailyStatusReportRepository : IEmployeeDailyStatusReport
    {
        SqlHelper objHelper = new SqlHelper();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public List<ProjectReportCategoryDomainModel> GetProjectReportCategoryByDeptId(long DepartmentId)
        {
            try
            {
                var list = objHelper.Query<ProjectReportCategoryDomainModel>("GetProjectReportCategoryByDeptId", new { DepartmentId = DepartmentId }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<EmployeeDailyReportDomainModel> GetDailyStatusReportDetailsOfCurrentDate(long employeeid)
        {
            try
            {
                var list = objHelper.Query<EmployeeDailyReportDomainModel>("GetDailyStatusReportDetailsOfCurrentDate", new { employeeid = employeeid, ReportDate = DateTime.Now, ReportDate1 = DateTime.Now.AddHours(-8) }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<EmployeeTotalWorkingHoursReportDomainModel> GetEmployeeTotalWorkingHoursReport(long DepartmentId, long EmployeeId, string StartDate, string EndDate)
        {
            try
            {
                var list = objHelper.Query<EmployeeTotalWorkingHoursReportDomainModel>("GET_EMPLOYEE_TOTALWORKINGHOURS_REPORT_NEW", new { departmentid = DepartmentId, employeeid = EmployeeId, startdate = string.IsNullOrWhiteSpace(StartDate)?"":StartDate, enddate = string.IsNullOrWhiteSpace(EndDate) ? "" : EndDate }).ToList();
                if (list != null && list.Count > 0)
                {
                   foreach(var obj in list)
                    {
                        obj.listEmployeeDailyReportDetails = GetEmployeeDailyReportDetails(DepartmentId, EmployeeId, obj.CreatedDate);
                        if (obj.listEmployeeDailyReportDetails != null && obj.listEmployeeDailyReportDetails.Count > 0)
                        {
                            long totalWorkingMinutes = obj.listEmployeeDailyReportDetails.Select(s => HelperMethods.ConversionInMinute(s.WorkingHours)).Sum();
                            obj.TotalWorking = Convert.ToString(HelperMethods.ConversionInHour(totalWorkingMinutes));
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<GetEmployeeDailyStatusReportDomainModel> GetEmployeeDailyReportDetails(long DepartmentId, long EmployeeId, string ReportDate)
        {
            try
            {
                var list = objHelper.Query<GetEmployeeDailyStatusReportDomainModel>("GET_EMPLOYEE_REPORT", new { departmentid = DepartmentId, employeeid = EmployeeId, date = ReportDate }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public ResponseDomainModel AddUpdateEmployeeDailyReport(AddEmployeeDailyStatusReportDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                if (model != null && model.listEmployeeDailyStatusReport != null && model.listEmployeeDailyStatusReport.Count > 0)
                {
                    foreach (EmployeeDailyReportDomainModel obj in model.listEmployeeDailyStatusReport)
                    {
                        if (obj.ProjectId > 0)
                        {
                            var add = objHelper.Query<string>("AddUpdateEmployeeDailyReport", new
                            {
                                DailyReportId = obj.DailyReportId,
                                EmployeeId = obj.EmployeeId,
                                ReportDate = DateTime.Now,
                                ProjectId = obj.ProjectId,
                                ReportCategoryId = obj.ReportCategoryId,
                                Description = obj.Description,
                                WorkingHours = obj.WorkingHours,
                                Filename = obj.Filename,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                CreatedBy = obj.CreatedBy
                            }).FirstOrDefault();
                            if (add == "Insert")
                            {
                                objRes.isSuccess = true;
                                objRes.response = "Status Report Saved Successfully.";
                            }
                            else if (add == "Update")
                            {
                                objRes.isSuccess = true;
                                objRes.response = "Status Report Updated Successfully.";
                            }
                            else
                            {
                                objRes.isSuccess = false;
                                objRes.response = "Status Report not saved.";
                            }
                        }
                    }
                }
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
