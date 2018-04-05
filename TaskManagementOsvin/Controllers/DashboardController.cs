using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskManagementOsvin.Models;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        // GET: Dashboard
        //static GetDSRModel GetDSRModel = new GetDSRModel() { startdate = DateTime.Now.AddDays(-48).ToString("dd/MM/yyyy"), enddate = DateTime.Now.AddDays(-40).ToString("dd/MM/yyyy") };
        static GetDSRModel GetDSRModel = new GetDSRModel() { startdate = DateTime.Now.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday).ToString("dd/MM/yyyy"), enddate = DateTime.Now.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Saturday).ToString("dd/MM/yyyy") };
        public ActionResult Welcome()
        {
            return View(GetDSRModel);
        }

        [HttpPost]
        public ActionResult Welcome(GetDSRModel Dsrmodel)
        {
            GetDSRModel.startdate = DateTime.ParseExact(Dsrmodel.startdate, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            GetDSRModel.enddate = DateTime.ParseExact(Dsrmodel.enddate, "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
            return View(GetDSRModel);
        }
        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        public PartialViewResult _projectReport()
        {
            decimal weekTotal = 0, OverallTotal = 0;
            var reports = GetProjectReport(GetDSRModel);
            if (UserManager.user.roleType == roleTypeModel.TeamLeader && reports.dsr != null && reports.dsr.Count() > 0)
            {
                reports.dsr = reports.dsr.Where(x => x.DepartmentId == UserManager.user.DepartmentId).ToList();
            }
            foreach (var item in reports.dsr)
            {
                weekTotal += ConversionInMinute(item.WorkingHoursOfProject);
            }
            foreach (var workingHours in reports.dsr)
            {
                OverallTotal += ConversionInMinute(workingHours.TotalWorkingHoursOfProject);
            }
            reports.OverallTotalWorkingHours = ConversionInHour(weekTotal);
            reports.OverallWeekTotalWorkingHours = ConversionInHour(OverallTotal);
            return PartialView(reports);
        }
        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        public PartialViewResult _employeeReport()
        {
            decimal TotalWeekelyWorkingHours = 0;
            List<WeekelyEmployeeReportModel> EmployeeReports = new List<WeekelyEmployeeReportModel>(); 
            var client = new HttpClient();
            var serialized = new JavaScriptSerializer().Serialize(GetDSRModel);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync(BaseURL + "/api/Employee/GetWeekelySummaryOfEmpDetails", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var reports = new JavaScriptSerializer().Deserialize<List<GetWeekelySummaryOfEmpModel>>(contents);
                if (UserManager.user.roleType == roleTypeModel.TeamLeader)
                {
                    reports = reports.Where(x => x.DepartmentId == UserManager.user.DepartmentId).ToList();
                }
                var GroupedReports = reports.GroupBy(x => x.EmployeeId).ToList();
                foreach (var item in GroupedReports)
                {
                    var empId = item.FirstOrDefault().EmployeeId;
                    var GetEmpReport = reports.Where(x => x.EmployeeId == empId).ToList();
                    var MondayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Monday).ToList();
                    var TuesdayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Tuesday).ToList();
                    var WednesdayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Wednesday).ToList();
                    var ThursdayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Thursday).ToList();
                    var FridayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Friday ).ToList();
                    var SaturdayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Saturday).ToList();
                    var SundayReports = GetEmpReport.Where(x => x.CreatedDate.DayOfWeek == DayOfWeek.Sunday).ToList();
                    if (MondayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in MondayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Monday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                    if (TuesdayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in TuesdayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Tuesday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                    if (WednesdayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in WednesdayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Wednesday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                    if (ThursdayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in ThursdayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Thursday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                    if (FridayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in FridayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Friday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                    if (SaturdayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in SaturdayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Saturday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                    if (SundayReports.Count() > 0)
                    {
                        decimal mins = 0;
                        foreach (var report in SundayReports)
                        {
                            mins += ConversionInMinute(report.WorkingHours);
                        }
                        var ReportModel = new WeekelyEmployeeReportModel()
                        {
                            EmployeeId = item.FirstOrDefault().EmployeeId,
                            DepartmentName = item.FirstOrDefault().DepartmentName,
                            Name = item.FirstOrDefault().Name,
                            ProjectTitle = item.FirstOrDefault().ProjectTitle,
                            WorkingHours = ConversionInHour(mins),
                            DayOfWeek = DayOfWeek.Sunday
                        };
                        EmployeeReports.Add(ReportModel);
                        var totalWeekelyMins = ConversionInMinute(TotalWeekelyWorkingHours.ToString()) + mins;
                        TotalWeekelyWorkingHours = ConversionInHour(totalWeekelyMins);
                    }
                }
            }
            return PartialView(EmployeeReports);
        }

        public ActionResult FullProjectReport(long ProjectId = 0)
        {
            if (ProjectId > 0)
            {
                var overallWorkingHrs = new List<HoursByDateAndProjModel>();
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                StringBuilder sb = new StringBuilder();
                sb.Append("<table class='table table-bordered table-striped table-condensed flip-content'>");
                sb.Append("<thead>");
                // heading

                var Clientresult = client.GetAsync(BaseURL + "/api/Project/GetDepartmentAndEmployeeInProject/" + ProjectId).Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    sb.Append("<tr>");
                    sb.Append("<th></th>");
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<List<GetDepAndEmpProjModel>>(contents);
                    foreach (var item in response)
                    {
                        sb.Append("<th class='text-center' colspan='" + item.ColumnSpan + "'>");
                        sb.Append(item.DepartmentName);
                        sb.Append("</th>");
                    }
                    sb.Append("<th></th>");
                    sb.Append("</tr>");
                }
                List<long> UserList = new List<long>();
                var EmployeesWorked = client.GetAsync(BaseURL + "/api/Project/EmployeesWorkedOnProject/" + ProjectId).Result;
                if (EmployeesWorked.StatusCode == HttpStatusCode.OK)
                {
                    sb.Append("<tr>");
                    sb.Append("<th>Date</th>");
                    var contents = EmployeesWorked.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<List<EmpWorkedOnProjModel>>(contents);
                    foreach (var item in response)
                    {
                        sb.Append("<th>");
                        var names = item.EmployeeName.Split(' ');
                        foreach (var name in names)
                        {
                            if (name != "")
                            {
                                sb.Append(name + "<br />");
                            }
                        }
                        sb.Append("</th>");
                        //--------
                        overallWorkingHrs.Add(new HoursByDateAndProjModel() { DepartmentName = item.DepartmentName, EmployeeName = item.EmployeeName, WorkingHours = "0.0" });
                        //-----------------------
                        UserList.Add(item.UserId);
                    }
                    sb.Append("<th>Week Total</th>");
                    sb.Append("</tr>");
                }
                sb.Append("</thead>");
                sb.Append("<tbody>");
                // body here
                var WeeksBetweenDatesresult = client.GetAsync(BaseURL + "/api/Project/WeeksBetweenDates/" + ProjectId).Result;
                if (WeeksBetweenDatesresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = WeeksBetweenDatesresult.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<List<WeeksBwDateModel>>(contents);
                    foreach (var item in response)
                    {
                        decimal totalWorkingMins = 0;
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append(item.StartDate.ToString("dd-MMM-yyyy") + "<br />" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To" + "<br />" + item.EndDate.ToString("dd-MMM-yyyy"));
                        sb.Append("</td>");

                        GetHrsByDateAndProjModel model = new GetHrsByDateAndProjModel() { Projectid = ProjectId, startdate = item.StartDate.ToString("dd/MM/yyyy").ToString(), enddate = item.EndDate.ToString("dd/MM/yyyy").ToString() };
                        var serialized = new JavaScriptSerializer().Serialize(model);
                        var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                        var WorkedHoursresult = client.PostAsync(BaseURL + "/api/Project/GetHoursBetweenTwoDates", content).Result;
                        if (WorkedHoursresult.StatusCode == HttpStatusCode.OK)
                        {
                            totalWorkingMins = 0;
                            var WorkedHourscontents = WorkedHoursresult.Content.ReadAsStringAsync().Result;
                            var WorkedHoursresponse = JsonConvert.DeserializeObject<List<HoursByDateAndProjModel>>(WorkedHourscontents);
                            foreach (var userId in UserList)
                            {
                                sb.Append("<td>");
                                var user = WorkedHoursresponse.FirstOrDefault(x => x.UserId == userId);
                                if (user != null)
                                {
                                    //----------
                                    var getUser = overallWorkingHrs.First(x => x.DepartmentName == user.DepartmentName && x.EmployeeName == user.EmployeeName);
                                    var dec = ConversionInMinute(user.WorkingHours) + ConversionInMinute(getUser.WorkingHours);
                                    getUser.WorkingHours = ConversionInHour(dec).ToString();
                                    //---------
                                    totalWorkingMins += ConversionInMinute(user.WorkingHours);
                                    sb.Append(user.WorkingHours);
                                }
                                else
                                {
                                    sb.Append("0");
                                }
                                sb.Append("</td>");

                            }
                        }
                        sb.Append("<td>" + ConversionInHour(totalWorkingMins) + "</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("<tfoot>");
                sb.Append("<tr>");
                sb.Append("<th>");
                sb.Append("</th>");
                foreach (var item in overallWorkingHrs)
                {
                    sb.Append("<th>" + item.WorkingHours + "</th>");
                }
                sb.Append("<th>");
                sb.Append("</th>");
                sb.Append("</tr>");
                sb.Append("</tfoot>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                ViewBag.Table = sb;
            }
            return View();
        }

        [NonAction]
        public SummaryDSRModel GetProjectReport(GetDSRModel model)
        {
            SummaryDSRModel Summarymodel = new SummaryDSRModel();
            var client = new HttpClient();
            var serialized = new JavaScriptSerializer().Serialize(model);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync(BaseURL + "/api/Employee/GetSummaryOfWeekDetails", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                Summarymodel.dsr = new JavaScriptSerializer().Deserialize<List<GetWeeklyEmployeeDSRModel>>(contents);
            }
            return Summarymodel;
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            ViewBag.Class = "display-hide";
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordReqModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = UserManager.user.UserId;
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync(BaseURL + "/api/Employee/ChangePassword", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                        ModelState.Clear();
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.AlertType = "alert-success";
                        ViewBag.Class = null;
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.Class = null;
                        ViewBag.AlertType = "alert-danger";
                    }
                    else if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        ModelState.AddModelError("CustomError", "Old Password didn't match.");
                        ViewBag.Class = null;
                        ViewBag.AlertType = "alert-danger";
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "Error occurred");
                        ViewBag.Class = null;
                        ViewBag.AlertType = "alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Class = null;
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.AlertType = "alert-danger";
            }
            return View();
        }

        [NonAction]
        public decimal ConversionInHour(decimal durationMin)
        {
            decimal hours = (durationMin - durationMin % 60) / 60;
            var hoursFormat = string.Format("{0:00}:{1:00}", (int)hours, durationMin % 60);
            //hoursFormat
            decimal total = Convert.ToDecimal(hoursFormat.Replace(':', '.'));
            return total;
        }

        [NonAction]
        public decimal ConversionInMinute(string WorkingHours)
        {

            string[] parts = WorkingHours.Split('.');
            decimal hours = 0;
            decimal minutes = 0;
            if (parts.Length > 1)
            {
                hours = Convert.ToDecimal(parts[0]);
                minutes = Convert.ToDecimal((parts[1]));
            }
            else
            {
                hours = Convert.ToDecimal(parts[0]);
                minutes = 0;
            }

            decimal TotalMintues = hours * 60 + minutes;
            return TotalMintues;
        }

        [NonAction]
        public OverAllSummaryOfWeekDetailsMainModel GetSummaryMain(GetSummaryModel model)
        {
            var serialized = new JavaScriptSerializer().Serialize(model);
            var client = new HttpClient();
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync(BaseURL + "/api/Employee/SummaryOfWeekDetails", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<OverAllSummaryOfWeekDetailsMainModel>(contents);
                return Response;
            }
            return null;
        }

       
    }
}