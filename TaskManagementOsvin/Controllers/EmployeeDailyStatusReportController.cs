using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
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
        // GET: EmployeeDailyStatusReport
        public ActionResult AddEmployeeDailyStatusReport()
        {
            ViewBag.Class = "display-hide";
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            List<ProjectReportCategoryDomainModel> listTaskCategories = new List<ProjectReportCategoryDomainModel>();
            AddEmployeeDailyReportModel model = new AddEmployeeDailyReportModel();

            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client.GetAsync("/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                listProjects = response;
                listProjects = listProjects.Where(p => p.ProjectId != 11).ToList();
            }
            var TaskCategoryResult = client.GetAsync("/api/EmployeeDailyStatusReport/GetProjectReportCategoryByDeptId?DepartmentId=" + UserManager.user.DepartmentId).Result;
            if (TaskCategoryResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = TaskCategoryResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectReportCategoryDomainModel>>(contents);
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
                        obj.EmployeeId = UserManager.user.UserId;
                        obj.WorkingHours = obj.Hours + "." + obj.Minutes;
                        obj.CreatedBy = UserManager.user.UserId;
                        obj.Filename = string.Empty;
                    }
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/EmployeeDailyStatusReport/AddUpdateEmployeeDailyReport", content).Result;
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
            var ProjectResult = client.GetAsync("/api/EmployeeDailyStatusReport/GetDailyStatusReportDetailsOfCurrentDate?EmployeeId="+UserManager.user.UserId).Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmployeeDailyReportModel>>(contents);
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
            if(!string.IsNullOrWhiteSpace(StartDate))
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
            var res = client.GetAsync("/api/EmployeeDailyStatusReport/GetEmployeeTotalWorkingHoursReport?DepartmentId=" + UserManager.user.DepartmentId +"&EmployeeId=" + UserManager.user.UserId+"&StartDate="+ StartDate + "&EndDate="+EndDate).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var contents = res.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmployeeTotalWorkingHoursReportDomainModel>>(contents);
                listEmployeeTotalWorkingHoursReport = response;
            }
            return PartialView(listEmployeeTotalWorkingHoursReport);
        }
    }
}