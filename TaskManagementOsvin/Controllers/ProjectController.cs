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
using Providers.Helper;
using Newtonsoft.Json;
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
            List<FeedbackDomainModel> listProjectFeedback = new List<FeedbackDomainModel>();
            if (ProjectId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.GetAsync("/api/Feedback/GetProjectFeedback?ProjectId=" + ProjectId).Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FeedbackDomainModel>>(contents);
                    listProjectFeedback = response;
                }
            }
            return PartialView(listProjectFeedback);
        }
        [HttpGet]
        public ActionResult _EmployeeWorkedonProject(long ProjectId)
        {
            List<ProjectAssignToUserTotalWorkingHoursDomainModel> listProjectAssignToUser = new List<ProjectAssignToUserTotalWorkingHoursDomainModel>();
            if (ProjectId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.GetAsync("/api/Project/GetProjectAssignToUserWithTotalWorkingHours?ProjectId=" + ProjectId + "&Status=ProjectReport").Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectAssignToUserTotalWorkingHoursDomainModel>>(contents);
                    listProjectAssignToUser = response;
                }
            }
            if (listProjectAssignToUser != null && listProjectAssignToUser.Count > 0)
            {
                int TotalWorkingMin = Convert.ToInt32(HelperMethods.ConversionInMinute(listProjectAssignToUser[0].TotalWorkingHours.ToString()));
                listProjectAssignToUser.ForEach(s => s.TotalWorkingHours = HelperMethods.ConversionInHour(TotalWorkingMin));
            }
            return PartialView(listProjectAssignToUser);
        }
        [HttpGet]
        public ActionResult AddWorkingHoursOfProjectBeforePMS()
        {
            ProjectWorkingHoursBeforePMSModel objModel = new ProjectWorkingHoursBeforePMSModel();
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client.GetAsync("/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                listProjects = response;
            }
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            ViewBag.Class = "display-hide";
            return View(objModel);
        }
        [HttpPost]
        public ActionResult AddWorkingHoursOfProjectBeforePMS(ProjectWorkingHoursBeforePMSModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = Convert.ToString(UserManager.user.UserId);
                    var serialized = JsonConvert.SerializeObject(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/Project/AddProjectWorkingHoursBeforePms", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.Clear();
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.AlertType = "alert-success";
                        ViewBag.Class = null;
                        model = new ProjectWorkingHoursBeforePMSModel();
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
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
                ErrorLog.LogError(ex);
                ViewBag.Class = null;
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.AlertType = "alert-danger";
            }
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            var client1 = new HttpClient();
            client1.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client1.GetAsync("/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                listProjects = response;
            }
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            return View(model);
        }
        [HttpGet]
        public ActionResult ProjectTaskCategories()
        {
            ViewBag.Class = "display-hide";
            List<DepartmentDomainModel> listDepartments = new List<DepartmentDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync("/api/Management/GetAllDepartments").Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<DepartmentDomainModel>(contents);
                listDepartments = Response.listDepartments;
            }
            ViewBag.listDepartments = new SelectList(listDepartments, "DepartmentId", "DepartmentName");
            return View();
        }
        [HttpGet]
        public ActionResult _ProjectTaskCategories()
        {
            List<ProjectReportCategoryDomainModel> listProjectReportCategories = new List<ProjectReportCategoryDomainModel>();
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync("/api/Project/GetProjectReportCategories").Result;
            if (Clientresult.StatusCode == HttpStatusCode.OK)
            {
                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectReportCategoryDomainModel>>(contents);
                listProjectReportCategories = response;
            }
            return PartialView(listProjectReportCategories);
        }
        [HttpPost]
        public ActionResult AddUpateProjectTaskCategories(ProjectReportCategoryDomainModel model)
        {
            if (model != null)
            {
                model.CreatedBy = UserManager.user.UserId;
                model.CreatedDate = DateTime.Now;
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync("/api/Project/AddUpdateReportCategory", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_ProjectTaskCategories");
        }
        [HttpGet]
        public ActionResult DeleteProjectReportCategory(long CategoryId)
        {
            if (CategoryId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync("/api/Project/DeleteProjectReportCategory?CategoryId=" + CategoryId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_ProjectTaskCategories");
        }
        [HttpGet]
        public ActionResult ActivateDeactivateProjectReportCategory(long CategoryId, bool IsActive)
        {
            if (CategoryId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync("/api/Project/ActivateDeactivateProjectReportCategory?CategoryId=" + CategoryId + "&IsActive=" + IsActive).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_ProjectTaskCategories");
        }
        [HttpGet]
        public ActionResult AddProjectAddendumDetails()
        {
            ViewBag.Class = "display-hide";
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            listProjects = GetProjectList();
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            return View();
        }

        [HttpPost]
        public ActionResult AddProjectAddendumDetails(AddendumModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    string path = Server.MapPath("~/UploadDocuments/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }                    
                    if (model.UploadDocumentFile != null)
                    {
                        string fileName = Path.GetFileName(model.UploadDocumentFile.FileName);
                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.UploadDocumentFile.FileName);
                        model.UploadDocumentFile.SaveAs(path + fileName);
                        model.UploadDocument = fileName;
                        model.UploadDocumentFile = null;
                    }
                    var serialized = JsonConvert.SerializeObject(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/Project/AddProjectTimeEstimation", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.Clear();
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.AlertType = "alert-success";
                        ViewBag.Class = null;
                        model = new AddendumModel();
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
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
                ErrorLog.LogError(ex);
                ViewBag.Class = null;
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.AlertType = "alert-danger";
            }
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            listProjects = GetProjectList();
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            return View(model);
        }
        [HttpGet]
        public ActionResult MergeProject()
        {
            ViewBag.Class = "display-hide";
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            listProjects = GetProjectList();
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            return View();
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

       
        #region User Defined Functions
        public List<ProjectDomainModel> GetProjectList()
        {
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            var client1 = new HttpClient();
            client1.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client1.GetAsync("/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                listProjects = response;
            }
            return listProjects;
        }
        #endregion
    }
}