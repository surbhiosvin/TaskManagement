using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TaskManagementOsvin.Models;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    public class EmployeeDailyStatusReportController : Controller
    {
        // GET: EmployeeDailyStatusReport
        public ActionResult AddEmployeeDailyStatusReport()
        {
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
                model.listEmployeeDailyStatusReport.Add(new EmployeeDailyReportModel { ReportCategoryId = 0, ProjectId = 0, Minutes = 00, Hours = 0 });
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

            ViewData["ddlProject"] = listProjects;
            ViewData["ddlTaskCategories"] = listTaskCategories;
            ViewData["ddlHours"] = listHours;
            ViewData["ddlMinutes"] = listMinutes;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEmployeeDailyStatusReport(AddEmployeeDailyReportModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.listEmployeeDailyStatusReport != null && model.listEmployeeDailyStatusReport.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.Class = "alert-danger";
            }
            return View();
        }
    }
}