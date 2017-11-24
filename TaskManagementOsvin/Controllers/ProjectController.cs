using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementOsvin.Security;
using TaskManagementOsvin.Models;
using System.Net.Http;
using System.Net;
using System.Web.Script.Serialization;
using DomainModel.EntityModel;

namespace TaskManagementOsvin.Controllers
{
    //[Authorize] // assign roles
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult AddUpdateProject(int ProjectId = 0)
        {
            AddUpdateProjectModel model = new AddUpdateProjectModel();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.GetAsync("/api/Client/GetClients").Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = new JavaScriptSerializer().Deserialize<List<ClientModel>>(contents);
                    ViewBag.Clients = response;
                }
                var ProjectTypeResult = client.GetAsync("/api/Project/GetProjectType").Result;
                if (ProjectTypeResult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = ProjectTypeResult.Content.ReadAsStringAsync().Result;
                    var response = new JavaScriptSerializer().Deserialize<List<ProjectTypeModel>>(contents);
                    ViewBag.ProjectType = response;
                }
                var employeesResult = client.GetAsync("/api/Employee/GetEmployees").Result;
                if (employeesResult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = employeesResult.Content.ReadAsStringAsync().Result;
                    var response = new JavaScriptSerializer().Deserialize<List<EmployeesModel>>(contents);
                    ViewBag.Employees = response;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddUpdateProject(AddUpdateProjectModel model)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult ProjectFullDetails(long ProjectId)
        {
            ViewBag.ProjectId = ProjectId;    
            return View();
        }
        [HttpGet]
        public ActionResult _ProjectFullDetail(long ProjectId)
        {
            ProjectFullDetailsDomainModel model = new ProjectFullDetailsDomainModel();
            if (ProjectId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.GetAsync("/api/Project/GetProjectFullDetails?ProjectId=" + ProjectId).Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectFullDetailsDomainModel>(contents);
                    model = response;
                }
            }
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult _ProjectAddendumDetails(long ProjectId)
        {
            List<AddendumDomainModel> listAddendums = new List<AddendumDomainModel>();
            if (ProjectId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.GetAsync("/api/Project/GetProjectAddendumDetails?ProjectId=" + ProjectId).Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AddendumDomainModel>>(contents);
                    listAddendums = response;
                }
            }
            return PartialView(listAddendums);
        }
        [HttpGet]
        public ActionResult _ProjectFeedBackDetails(long ProjectId)
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult _EmployeeWorkedonProject()
        {
            return PartialView();
        }

    }
}