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
                var list = objHelper.Query<EmployeeTotalWorkingHoursReportDomainModel>("GET_EMPLOYEE_TOTALWORKINGHOURS_REPORT_NEW", new { departmentid = DepartmentId, employeeid = EmployeeId, startdate = string.IsNullOrWhiteSpace(StartDate) ? "" : StartDate, enddate = string.IsNullOrWhiteSpace(EndDate) ? "" : EndDate }).ToList();
                if (list != null && list.Count > 0)
                {
                    foreach (var obj in list)
                    {
                        obj.listEmployeeDailyReportDetails = GetEmployeeDailyReportDetails(DepartmentId, EmployeeId, obj.CreatedDate);
                        if (obj.listEmployeeDailyReportDetails != null && obj.listEmployeeDailyReportDetails.Count > 0)
                        {
                            long totalWorkingMinutes = obj.listEmployeeDailyReportDetails.Select(s => HelperMethods.ConversionInMinute(s.WorkingHours)).Sum();
                            obj.TotalWorking = Convert.ToString(HelperMethods.ConversionInHour(totalWorkingMinutes));
                        }
                    }
                    long totalWorkingMinutesTillDate = list.Select(s => HelperMethods.ConversionInMinute(s.TotalWorking)).Sum();
                    list[0].EmployeeTotalWorkingHoursTillDate = Convert.ToString(HelperMethods.ConversionInHour(totalWorkingMinutesTillDate));
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
                var list = objHelper.Query<GetEmployeeDailyStatusReportDomainModel>("GET_EMPLOYEE_REPORT_NEW", new { departmentid = DepartmentId, employeeid = EmployeeId, date = ReportDate }).ToList();
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
                                CreatedBy = obj.CreatedBy,
                                IsByTeamLeader = false
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
        public ResponseDomainModel AddDailyStatusReportByTeamLeader(EmployeeDailyReportDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            try
            {
                if (model != null)
                {
                    var add = objHelper.Query<string>("AddUpdateEmployeeDailyReport", new
                    {
                        DailyReportId = model.DailyReportId,
                        EmployeeId = model.EmployeeId,
                        ReportDate = DateTime.Now,
                        ProjectId = model.ProjectId,
                        ReportCategoryId = model.ReportCategoryId,
                        Description = model.Description,
                        WorkingHours = model.WorkingHours,
                        Filename = model.Filename,
                        IsActive = true,
                        CreatedDate = model.CreatedDate,
                        CreatedBy = model.CreatedBy,
                        IsByTeamLeader=true
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
                return objRes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<EmployeeTotalWorkingHoursReportDomainModel> GetEmployeeReportFilledByTeamLeaderWithTotalWorkingHours(long DepartmentId, long TLEmployeeId)
        {
            try
            {
                var list = objHelper.Query<EmployeeTotalWorkingHoursReportDomainModel>("GET_EMPLOYEE_REPORT_FILLED_BY_TEAMLEADER_WITH_TOTALWORKING_HOURS", new { departmentid = DepartmentId, employeeid = TLEmployeeId }).ToList();
                if (list != null && list.Count > 0)
                {
                    foreach (var obj in list)
                    {
                        obj.listEmployeeDailyReportDetails = GetEmployeeReportDetailsFilledByTeamLeader(DepartmentId, TLEmployeeId, obj.UserId, obj.CreatedDate);
                        if (obj.listEmployeeDailyReportDetails != null && obj.listEmployeeDailyReportDetails.Count > 0)
                        {
                            long totalWorkingMinutes = obj.listEmployeeDailyReportDetails.Select(s => HelperMethods.ConversionInMinute(s.WorkingHours)).Sum();
                            obj.TotalWorking = Convert.ToString(HelperMethods.ConversionInHour(totalWorkingMinutes));
                            obj.listEmployeeDailyReportDetails.ForEach(s => s.EmployeeId = obj.UserId);
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
        public List<GetEmployeeDailyStatusReportDomainModel> GetEmployeeReportDetailsFilledByTeamLeader(long DepartmentId, long TLEmployeeId, long EmployeeId, string CreatedDate)
        {
            try
            {
                var list = objHelper.Query<GetEmployeeDailyStatusReportDomainModel>("GET_EMPLOYEE_REPORT_FILLED_BY_TEAMLEADER_NEW", new { departmentid = DepartmentId, tlemployeeid = TLEmployeeId, employeeid = EmployeeId, date = CreatedDate }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<EmployeeTotalWorkingHoursReportDomainModel> GetEmployeeReportFilledByTeamLeaderAccToEmployeeId(long TLEmployeeId, long EmployeeId)
        {
            try
            {
                long DepartmentId = 0;
                if(TLEmployeeId>0)
                {
                    IManagement managementRepository = new ManagementRepository();
                    var emp = managementRepository.GetEmployeeDataById(TLEmployeeId);
                    if(emp!=null && emp.UserId>0)
                    {
                        DepartmentId = emp.DepartmentId;
                    }
                }
                var list = objHelper.Query<EmployeeTotalWorkingHoursReportDomainModel>("GET_EMPLOYEE_REPORT_FILLED_BY_TEAMLEADER_ACCORDING_TO_EMPLOYEEID", new { departmentid = DepartmentId, teamleaderid = TLEmployeeId, employeeid = EmployeeId }).ToList();
                if (list != null && list.Count > 0)
                {
                    foreach (var obj in list)
                    {
                        obj.listEmployeeDailyReportDetails = GetEmployeeReportAccordingToEmployeeId(DepartmentId, TLEmployeeId, EmployeeId, obj.CreatedDate);
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
        public List<GetEmployeeDailyStatusReportDomainModel> GetEmployeeReportAccordingToEmployeeId(long DepartmentId, long TLEmployeeId, long EmployeeId, string CreatedDate)
        {
            try
            {
                var list = objHelper.Query<GetEmployeeDailyStatusReportDomainModel>("GET_EMPLOYEE_REPORT_ACCORDINGTO_EMPLOYEEID", new { departmentid = DepartmentId, teamleaderid = TLEmployeeId, employeeid = EmployeeId, date = CreatedDate }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public EmployeeDailyReportDomainModel GetDailyStatusReportDetailsById(long DailyReportId)
        {
            try
            {
                var list = objHelper.Query<EmployeeDailyReportDomainModel>("GET_DAILY_REPORT2_BY_ID", new { dailyreportid = DailyReportId }).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
        public List<EmployeeTotalWorkingHoursInMonthDomainModel> GetEmployeeTotalWorkingHours(string StartDate, string EndDate)
        {
            try
            {
                var listEmployees = objHelper.Query<EmployeeTotalWorkingHoursInMonthDomainModel>("GetEmployeeWorkedInMonth", new { startdate = StartDate, enddate = EndDate }).ToList();
                if(listEmployees!=null && listEmployees.Count>0)
                {
                    foreach(var emp in listEmployees)
                    {
                        emp.listEmployeeHours = objHelper.Query<EmployeeHoursDomainModel>("GetEmployeeHours", new { EmployeeId = emp.EmployeeId, StartDate = StartDate, EndDate = EndDate }).ToList();
                        if(emp.listEmployeeHours!=null && emp.listEmployeeHours.Count>0)
                        {
                            long totalWorkingMinutes = emp.listEmployeeHours.Select(s => HelperMethods.ConversionInMinute(s.WorkingHours)).Sum();
                            emp.Hours = Convert.ToString(HelperMethods.ConversionInHour(totalWorkingMinutes));
                        }
                        emp.EmployeeName = emp.FirstName + " " + emp.LastName;
                    }
                }
                return listEmployees;
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
