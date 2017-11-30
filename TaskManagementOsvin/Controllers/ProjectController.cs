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
using System.IO;

namespace TaskManagementOsvin.Controllers
{
    //[Authorize] // assign roles
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult AddUpdateProject(int ProjectId = 0)
        {
            ViewBag.Class = "display-hide";
            GetProjectTypeAndClients();
            AddUpdateProjectModel model = new AddUpdateProjectModel();
            try
            {
                
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
            ViewBag.Class = "alert-danger";
            ViewBag.Message = "Added Successfully";
            GetProjectTypeAndClients();
            try
            {
                model.CreatedBy = UserManager.user.UserId;
                if (model.UploadDetailsDocumentFile != null && model.UploadDetailsDocumentFile.ContentLength > 0)
                {
                    var uniqueName = Guid.NewGuid();
                    model.UploadDetailsDocument = uniqueName + model.UploadDetailsDocumentFile.FileName;
                    model.UploadDetailsDocumentFile.SaveAs(Server.MapPath("~/UploadDocuments/" + model.UploadDetailsDocument));
                    model.UploadDetailsDocumentFile = null; // do null nor it will cause error while serializing
                }
                if (model.UploadDocumentFile != null && model.UploadDocumentFile.ContentLength > 0)
                {
                    var uniqueName = Guid.NewGuid();
                    model.UploadDocument = uniqueName + model.UploadDocumentFile.FileName;
                    model.UploadDocumentFile.SaveAs(Server.MapPath("~/UploadDocuments/" + model.UploadDocument));
                    model.UploadDocumentFile = null; // do null nor it will cause error while serializing
                }
                model.ProjectStatus = "Open";
                model.Archived = "NonArchive";
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.PostAsync("/api/Project/AddUpdateProject", content).Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    ViewBag.Class = "alert-success";
                    if (model.ProjectId > 0)
                    {
                        ViewBag.Message = "Updated Successfully";
                    }
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                    ModelState.Clear();
                } 
                else if (Clientresult.StatusCode == HttpStatusCode.NotImplemented)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                    ViewBag.Message = response.response;
                }
                else
                {
                    ViewBag.Message = "Error Occurred";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error Occurred";
                return View();
            }
        }

        private void GetProjectTypeAndClients()
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
        
        public ActionResult ProjectList()
        {
            return View();
        }
        
        public PartialViewResult _ProjectList(GetAllProjectsModel model)
        {
            List<AllProjectsModel> GetProjects = new List<AllProjectsModel>();
            var serialized = new JavaScriptSerializer().Serialize(model);
            var client = new HttpClient();
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync("/api/Project/GetProjectsByStatus", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                GetProjects = new JavaScriptSerializer().Deserialize<List<AllProjectsModel>>(contents);
            }
            return PartialView(GetProjects);
        }

    }
}