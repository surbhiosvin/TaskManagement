using DomainModel.EntityModel;
using Newtonsoft.Json;
using Providers.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskManagementOsvin.Models;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    [Authorize]
    public class EmployeeDailyStatusReportController : Controller
    {
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        // GET: EmployeeDailyStatusReport
        public ActionResult AddEmployeeDailyStatusReport(long EmployeeId = 0)
        {
            ViewBag.Class = "display-hide";
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            List<ProjectReportCategoryDomainModel> listTaskCategories = new List<ProjectReportCategoryDomainModel>();
            AddEmployeeDailyReportModel model = new AddEmployeeDailyReportModel();

            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client.GetAsync(BaseURL + "/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                listProjects = response;
                listProjects = listProjects.Where(p => p.ProjectId != 11).ToList();
            }
            long DepartmentId = 0;
            if (EmployeeId > 0)
            {
                var result = client.GetAsync(BaseURL + "/api/Management/GetEmployeeDataById?UserId=" + EmployeeId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<EmployeeModel>(contents);
                    if (Response != null && Response.UserId > 0)
                        DepartmentId = Response.DepartmentId;
                }
            }
            else
            {
                DepartmentId = UserManager.user.DepartmentId;
            }
            var TaskCategoryResult = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetProjectReportCategoryByDeptId?DepartmentId=" + DepartmentId).Result;
            if (TaskCategoryResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = TaskCategoryResult.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<ProjectReportCategoryDomainModel>>(contents);
                listTaskCategories = response;
            }
            for (int i = 0; i <= 3; i++)
            {
                model.listEmployeeDailyStatusReport.Add(new EmployeeDailyReportModel { ReportCategoryId = 0, ProjectId = 0, Minutes = "00", Hours = "" });
            }
            var listHours = new List<SelectListItem>();
            for (int i = 0; i < 24; i++)
            {
                listHours.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            var listMinutes = new List<SelectListItem>
            {
                new SelectListItem{ Text="00", Value = "00"},
                new SelectListItem{ Text="10", Value = "10"},
                new SelectListItem{ Text="15", Value = "15"},
                new SelectListItem{ Text="20", Value = "20"},
                new SelectListItem{ Text="25", Value = "25"},
                new SelectListItem {Text="30", Value="30" },
                new SelectListItem{ Text="35", Value = "35"},
                new SelectListItem{ Text="40", Value = "40"},
                new SelectListItem{ Text="45", Value = "45"},
                new SelectListItem{ Text="50", Value = "50"},
                new SelectListItem {Text="55", Value="55" }
            };
            ViewBag.listProjects = listProjects;
            ViewBag.listTaskCategories = listTaskCategories;
            ViewData["ddlHours"] = listHours;
            ViewData["ddlMinutes"] = listMinutes;
            return View(model);
        }
        [HttpPost]
        public JsonResult AddEmployeeDailyStatusReport(AddEmployeeDailyReportModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            ViewBag.Class = "display-hide";
            try
            {
                if (model.listEmployeeDailyStatusReport != null && model.listEmployeeDailyStatusReport.Count > 0)
                {
                    foreach (var obj in model.listEmployeeDailyStatusReport)
                    {
                        if (obj.EmployeeId == 0)
                            obj.EmployeeId = UserManager.user.UserId;
                        obj.WorkingHours = obj.Hours + "." + obj.Minutes;
                        obj.CreatedBy = UserManager.user.UserId;
                        obj.Filename = string.Empty;
                    }
                    var serialized = JsonConvert.SerializeObject(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync(BaseURL + "/api/EmployeeDailyStatusReport/AddUpdateEmployeeDailyReport", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        objRes = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                    }
                }
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("CustomError", ex.Message);
                //ViewBag.Class = "alert-danger";
            }
            return Json(objRes);
        }
        [HttpGet]
        public ActionResult _EmployeeDailyStatusReport()
        {
            ViewBag.Class = "display-hide";
            List<EmployeeDailyReportModel> listDailyStatusReports = new List<EmployeeDailyReportModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetDailyStatusReportDetailsOfCurrentDate?EmployeeId=" + UserManager.user.UserId).Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<EmployeeDailyReportModel>>(contents);
                listDailyStatusReports = response;
            }
            return PartialView(listDailyStatusReports);
        }
        public ActionResult DailystatusReportDetailsByEmployee()
        {
            return View();
        }
        public ActionResult _DailystatusReportDetailsByEmployee(string StartDate, string EndDate)
        {
            DateTime date = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                date = DateTime.ParseExact(StartDate, "dd/MM/yyyy", null);
                StartDate = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                date = DateTime.ParseExact(EndDate, "dd/MM/yyyy", null);
                EndDate = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            List<EmployeeTotalWorkingHoursReportDomainModel> listEmployeeTotalWorkingHoursReport = new List<EmployeeTotalWorkingHoursReportDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var res = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetEmployeeTotalWorkingHoursReport?DepartmentId=" + UserManager.user.DepartmentId + "&EmployeeId=" + UserManager.user.UserId + "&StartDate=" + StartDate + "&EndDate=" + EndDate).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var contents = res.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmployeeTotalWorkingHoursReportDomainModel>>(contents);
                listEmployeeTotalWorkingHoursReport = response;
            }
            return PartialView(listEmployeeTotalWorkingHoursReport);
        }
        public ActionResult AddDailyStatusReportByTeamLeader(long DailyReportId = 0)
        {
            ViewBag.Class = "display-hide";
            EmployeeDailyReportModel model = new EmployeeDailyReportModel();
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            List<ProjectReportCategoryDomainModel> listTaskCategories = new List<ProjectReportCategoryDomainModel>();
            try
            {
                model.Minutes = "00";
                if (DailyReportId > 0)
                {

                }
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var ProjectResult = client.GetAsync(BaseURL + "/api/Project/GetProjectList").Result;
                if (ProjectResult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                    listProjects = response;
                    listProjects = listProjects.Where(p => p.ProjectId != 11).ToList();
                }
                var TaskCategoryResult = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetProjectReportCategoryByDeptId?DepartmentId=" + UserManager.user.DepartmentId).Result;
                if (TaskCategoryResult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = TaskCategoryResult.Content.ReadAsStringAsync().Result;
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectReportCategoryDomainModel>>(contents);
                    listTaskCategories = response;
                }
                UserListSearchModel usermodel = new UserListSearchModel() { UserId = UserManager.user.UserId, Role = UserManager.user.Role, Name = "", Archived = "NonArchive" };
                List<EmployeeDomainModel> listusers = new List<EmployeeDomainModel>();
                var serialized = new JavaScriptSerializer().Serialize(usermodel);
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                var result = client.PostAsync(BaseURL + "/api/Management/UserListBySearch", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<UserListDomainModel>(contents);
                    listusers = Response.listUsers;
                }
                var listHours = new List<SelectListItem>();
                for (int i = 0; i < 24; i++)
                {
                    listHours.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                var listMinutes = new List<SelectListItem>
            {
                new SelectListItem{ Text="00", Value = "00"},
                new SelectListItem{ Text="10", Value = "10"},
                new SelectListItem{ Text="15", Value = "15"},
                new SelectListItem{ Text="20", Value = "20"},
                new SelectListItem{ Text="25", Value = "25"},
                new SelectListItem{Text="30", Value = "30" },
                new SelectListItem{ Text="35", Value = "35"},
                new SelectListItem{ Text="40", Value = "40"},
                new SelectListItem{ Text="45", Value = "45"},
                new SelectListItem{ Text="50", Value = "50"},
                new SelectListItem{Text="55", Value="55" }
            };
                ViewBag.listProjects = listProjects;
                ViewBag.listTaskCategories = listTaskCategories;
                ViewBag.listEmployees = listusers;
                ViewData["ddlHours"] = listHours;
                ViewData["ddlMinutes"] = listMinutes;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult AddDailyStatusReportByTeamLeader(EmployeeDailyReportModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            ViewBag.Class = "display-hide";
            try
            {
                if (model != null)
                {
                    model.WorkingHours = model.Hours + "." + model.Minutes;
                    model.CreatedBy = UserManager.user.UserId;
                    model.Filename = string.Empty;
                    model.CreatedDate = DateTime.ParseExact(model.ReportDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var serialized = JsonConvert.SerializeObject(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync(BaseURL + "/api/EmployeeDailyStatusReport/AddUpdateEmployeeDailyReportByTeamLeader", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        objRes = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return Json(objRes);
        }
        [HttpGet]
        public ActionResult _EmployeeDailyStatusReportByTeamLeader()
        {
            ViewBag.Class = "display-hide";
            List<EmployeeTotalWorkingHoursReportDomainModel> listDailyStatusReportsByTeamLeader = new List<EmployeeTotalWorkingHoursReportDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var res = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetEmployeeReportFilledByTeamLeaderWithTotalWorkingHours?DepartmentId=" + UserManager.user.DepartmentId + "&TLEmployeeId=" + UserManager.user.UserId).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var contents = res.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<EmployeeTotalWorkingHoursReportDomainModel>>(contents);
                listDailyStatusReportsByTeamLeader = response;
                listDailyStatusReportsByTeamLeader.ForEach(s => s.listEmployeeDailyReportDetails.ForEach(r => r.CreatedDateStr = r.CreatedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
            }
            return PartialView(listDailyStatusReportsByTeamLeader);
        }

        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        public ActionResult TeamLeaderFilledEmployeeDailyStatusReportDetails()
        {
            ViewBag.Class = "display-hide";
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var res = client.GetAsync(BaseURL + "/api/Management/GetAllTeamLeaders").Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var contents = res.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<EmployeeDomainModel>>(contents);
                ViewBag.listTeamLeaders = new SelectList(response, "UserId", "FirstName");
            }
            if (UserManager.user.roleType == roleTypeModel.TeamLeader)
            {
                var EmpRes = client.GetAsync(BaseURL + "/api/Management/GetEmployeeByTeamLeaderId?TeamLeaderId=" + UserManager.user.UserId).Result;
                if (EmpRes.StatusCode == HttpStatusCode.OK)
                {
                    var contents = EmpRes.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<List<EmployeeDomainModel>>(contents);
                    ViewBag.listEmployees = new SelectList(response, "UserId", "FirstName");
                }
            }     
            return View();
        }
        [HttpGet]
        public ActionResult _TeamLeaderFilledEmployeeDailyStatusReportDetails(long EmployeeId = 0, long TeamLeaderId = 0)
        {
            ViewBag.Class = "display-hide";
            if (TeamLeaderId == 0 && UserManager.user.roleType == roleTypeModel.TeamLeader)
                TeamLeaderId = UserManager.user.UserId;
            List<EmployeeTotalWorkingHoursReportDomainModel> listDailyStatusReportsByTeamLeader = new List<EmployeeTotalWorkingHoursReportDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var res = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetEmployeeReportFilledByTeamLeaderAccToEmployeeId?TLEmployeeId=" + TeamLeaderId + "&EmployeeId=" + EmployeeId).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var contents = res.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<EmployeeTotalWorkingHoursReportDomainModel>>(contents);
                listDailyStatusReportsByTeamLeader = response;
                if (listDailyStatusReportsByTeamLeader != null && listDailyStatusReportsByTeamLeader.Count > 0)
                {
                    listDailyStatusReportsByTeamLeader.ForEach(s => s.listEmployeeDailyReportDetails.ForEach(r => r.CreatedDateStr = r.CreatedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                    if (listDailyStatusReportsByTeamLeader[0].listEmployeeDailyReportDetails.Count > 0)
                    {
                        GetEmployeeDailyStatusReportDomainModel obj = listDailyStatusReportsByTeamLeader[0].listEmployeeDailyReportDetails[0];
                        if (obj != null)
                        {
                            ViewBag.EmpInfo = obj.EmployeeName + " " + obj.DepartmentName + " " + obj.DesignationName;
                        }
                    }
                }
            }  
            return PartialView(listDailyStatusReportsByTeamLeader);
        }

        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        public ActionResult EmployeeReport()
        {
            ViewBag.Class = "display-hide";
            List<DepartmentDomainModel> listDepartment = new List<DepartmentDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client.GetAsync(BaseURL + "/api/Management/GetAllDepartments").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<DepartmentDomainModel>(contents);
                listDepartment = response.listDepartments;
            }
            ViewBag.listDepartment = new SelectList(listDepartment, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpGet]
        public ActionResult _EmployeeReport(long DepartmentId = 0)
        {
            UserListSearchModel model = new UserListSearchModel() { UserId = UserManager.user.UserId, Role = UserManager.user.Role, Name = "", Archived = "NonArchive" };
            List<EmployeeDomainModel> listusers = new List<EmployeeDomainModel>();
            var serialized = new JavaScriptSerializer().Serialize(model);
            var client = new HttpClient();
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync(BaseURL + "/api/Management/UserListBySearch", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<UserListDomainModel>(contents);
                listusers = Response.listUsers.Where(s=>s.IsActive==true).ToList();
                if ((UserManager.user.roleType == roleTypeModel.Admin || UserManager.user.roleType == roleTypeModel.HR || UserManager.user.roleType == roleTypeModel.ProjectManager) && DepartmentId > 0)
                {
                    listusers = listusers.Where(s => s.DepartmentId == DepartmentId).ToList();
                }
            }
            return PartialView(listusers);
        }
        public ActionResult EmployeeReporting(long EmployeeId)
        {
            ViewBag.EmployeeId = EmployeeId;
            return View();
        }

        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        [HttpGet]
        public ActionResult _EmployeeReporting(long EmployeeId, string StartDate, string EndDate)
        {
            List<EmployeeTotalWorkingHoursReportDomainModel> listEmployeeTotalWorkingHoursReport = new List<EmployeeTotalWorkingHoursReportDomainModel>();
            if (EmployeeId > 0)
            {
                DateTime date = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(StartDate))
                {
                    date = DateTime.ParseExact(StartDate, "dd/MM/yyyy", null);
                    StartDate = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(EndDate))
                {
                    date = DateTime.ParseExact(EndDate, "dd/MM/yyyy", null);
                    EndDate = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                }
                long DepartmentId = 0;
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/GetEmployeeDataById?UserId=" + EmployeeId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<EmployeeModel>(contents);
                    if (Response != null && Response.UserId > 0)
                        DepartmentId = Response.DepartmentId;
                }
                var res = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetEmployeeTotalWorkingHoursReport?DepartmentId=" + DepartmentId + "&EmployeeId=" + EmployeeId + "&StartDate=" + StartDate + "&EndDate=" + EndDate).Result;
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var contents = res.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<List<EmployeeTotalWorkingHoursReportDomainModel>>(contents);
                    listEmployeeTotalWorkingHoursReport = response;
                    if (listEmployeeTotalWorkingHoursReport != null && listEmployeeTotalWorkingHoursReport.Count > 0)
                    {
                        listEmployeeTotalWorkingHoursReport.ForEach(s => s.listEmployeeDailyReportDetails.ForEach(r => r.CreatedDateStr = r.CreatedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                        listEmployeeTotalWorkingHoursReport.ForEach(s => s.listEmployeeDailyReportDetails.ForEach(r => r.Time = r.ReportDate.ToShortTimeString()));
                        GetEmployeeDailyStatusReportDomainModel obj = listEmployeeTotalWorkingHoursReport[0].listEmployeeDailyReportDetails[0];
                        if (obj != null)
                        {
                            ViewBag.EmpInfo = obj.EmployeeName + " " + obj.DepartmentName + " " + obj.DesignationName;
                        }
                    }
                }
                ViewBag.EmployeeId = EmployeeId;
            }
            return PartialView(listEmployeeTotalWorkingHoursReport);
        }

        [CustomAuthorize(roles: "HR,Admin")]
        public ActionResult EmployeeTotalWorkingHoursInMonth()
        {
            return View();
        }
        public ActionResult _EmployeeTotalWorkingHoursInMonth(string StartDate, string EndDate)
        {
            List<EmployeeTotalWorkingHoursInMonthDomainModel> listEmpTotalWorkingHoursInMonth = new List<EmployeeTotalWorkingHoursInMonthDomainModel>();
            DateTime date = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                date = DateTime.ParseExact(StartDate, "dd/MM/yyyy", null);
                StartDate = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                date = DateTime.ParseExact(EndDate, "dd/MM/yyyy", null);
                EndDate = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/EmployeeDailyStatusReport/GetEmployeeTotalWorkingHours?StartDate=" + StartDate + "&EndDate=" + EndDate).Result;
            if(result.StatusCode==HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<List<EmployeeTotalWorkingHoursInMonthDomainModel>>(contents);
                listEmpTotalWorkingHoursInMonth = response;
            }
            return PartialView(listEmpTotalWorkingHoursInMonth);
        }
    }
}