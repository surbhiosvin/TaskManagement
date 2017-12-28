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
using System.Globalization;

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
            try
            {
                if (ProjectId > 0)
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var Clientresult = client.GetAsync("/api/Project/GetProjectDetailsById?ProjectId=" + ProjectId).Result;
                    if (Clientresult.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = Clientresult.Content.ReadAsStringAsync().Result;
                        var response = new JavaScriptSerializer().Deserialize<GetProjectByIdModel>(contents);
                        var a = string.IsNullOrEmpty(response.EstimateHours) ? (decimal?)null : Convert.ToDecimal(response.EstimateHours);
                        AddUpdateProjectModel model = new AddUpdateProjectModel()
                        {
                            ProjectId = response.ProjectId,
                            ClientId = response.ClientId,
                            ProjectTypeId = response.ProjectTypeId,
                            ProjectTitle = response.ProjectTitle,
                            ProjectUrl = response.ProjectUrl,
                            HourlyRate = response.HourlyRate,
                            UploadDocument = response.UploadDocument,
                            StartDate = response.StartDate,
                            EndDate = response.EndDate,
                            EstimateHours = string.IsNullOrEmpty(response.EstimateHours) ? (decimal?)null : Convert.ToDecimal(response.EstimateHours),
                            AssignedHours = string.IsNullOrEmpty(response.AssignHours) ? (decimal?)null : Convert.ToDecimal(response.AssignHours),
                            developerCodinghours = string.IsNullOrEmpty(response.DeveloperCodingHours) ? (decimal?)null : Convert.ToDecimal(response.DeveloperCodingHours),
                            WebserviceHours = string.IsNullOrEmpty(response.WebServiceHours) ? (decimal?)null : Convert.ToDecimal(response.WebServiceHours),
                            EstimateDesignHours = string.IsNullOrEmpty(response.DesignHours) ? (decimal?)null : Convert.ToDecimal(response.DesignHours),
                            SEOHours = string.IsNullOrEmpty(response.SEOHours) ? (decimal?)null : Convert.ToDecimal(response.SEOHours),
                            MaintainenceHours = string.IsNullOrEmpty(response.MaintainenceHours) ? (decimal?)null : Convert.ToDecimal(response.MaintainenceHours),
                            NetworkSupportHours = string.IsNullOrEmpty(response.NetworkSupprotHours) ? (decimal?)null : Convert.ToDecimal(response.NetworkSupprotHours),
                            QualityAnalystHours = string.IsNullOrEmpty(response.QAHours) ? (decimal?)null : Convert.ToDecimal(response.QAHours),
                            UploadDetailsDocument = response.UploadDetailsDocument
                        };
                        return View(model);
                    }
                    else
                    {
                        ViewBag.Class = "alert-danger";
                        ViewBag.Message = "Error Occurred";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Class = "alert-danger";
                ViewBag.Message = ex.Message;
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
                if (ModelState.IsValid)
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

        public ActionResult GetProfiles(long ProjectId = 0)
        {
            try
            {
                if (ProjectId > 0)
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var Clientresult = client.GetAsync("/api/Profile/GetProfilesByProjectId/" + ProjectId).Result;
                    if (Clientresult.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = Clientresult.Content.ReadAsStringAsync().Result;
                        var response = new JavaScriptSerializer().Deserialize<List<GetProfilesModel>>(contents);
                        return View(response);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult GetProfiles(AddUpdateUpworkProfileModel model)
        //{
        //    return View();
        //}

        public ActionResult AddUpdateProfile(long ProjectId = 0)
        {
            GetProjectTypeAndProjects();
            return View();
        }

        [HttpPost]
        public JsonResult AddUpdateProfile(List<UpdateUpworkProfileModel> ProfileList = null)
        {
            var result = "Successful <br />";
            try
            {
                if (ProfileList != null)
                {
                    ProfileList.ForEach(x => { x.createdBy = UserManager.user.UserId; });
                    var qs = Request.UrlReferrer.Query;
                    if (qs != "")
                    {
                        var ProjectId = Convert.ToUInt64(qs.Split('=')[1]);
                        if (ProjectId > 0)
                        {
                            var client = new HttpClient();
                            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                            var Clientresult = client.GetAsync("/api/Profile/GetProfilesByProjectId/" + ProjectId).Result;
                            if (Clientresult.StatusCode == HttpStatusCode.OK)
                            {
                                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                                var response = new JavaScriptSerializer().Deserialize<List<GetProfilesModel>>(contents);
                                var selectIds = ProfileList.Select(x => x.ProfileId);
                                var idsToDel = response.Where(x => !selectIds.Contains(x.ProfileId)).Select(x=>x.ProfileId).ToArray();
                                string joinedIdsToDel = string.Join(",", idsToDel);

                                if (joinedIdsToDel != "")
                                {
                                    var Getresult = client.PostAsync("/api/Profile/DeleteProfiles/" + joinedIdsToDel, null).Result;
                                    if (Getresult.StatusCode == HttpStatusCode.InternalServerError)
                                    {
                                        return Json(new { isSuccess = false, reason = "Error occurred" });
                                    }
                                }
                                var idsNotToInsert = response.Where(x => selectIds.Contains(x.ProfileId)).Select(x => x.ProfileId).ToArray();
                                ProfileList.RemoveAll(x => idsNotToInsert.Contains(x.ProfileId));

                            }
                            else if (Clientresult.StatusCode == HttpStatusCode.InternalServerError)
                            {
                                return Json(new { isSuccess = false, reason = "Error occurred" });
                            }
                        }
                    }
                    
                    foreach (var model in ProfileList)
                    {
                        var client = new HttpClient();
                        client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                        var serialized = new JavaScriptSerializer().Serialize(model);
                        var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                        var Clientresult = client.PostAsync("/api/Profile/AddProfile", content).Result;
                        if (Clientresult.StatusCode == HttpStatusCode.OK)
                        {
                            var contents = Clientresult.Content.ReadAsStringAsync().Result;
                            var response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                            if (response.isSuccess)
                            {
                                result += "Profile with " + model.ProfileName + " is created <br />";
                            }
                        }
                        else if (Clientresult.StatusCode == HttpStatusCode.NotImplemented || Clientresult.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            result += "Profile with " + model.ProfileName + " is not created <br />";
                        }
                    }
                    return Json(new { isSuccess = true, reason = result });
                }
                return Json(new { isSuccess = false, reason = "List is empty" });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, reason = "Error occurred" });
            }
        }

        private void PopulateStatusList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Open", Value = "Open"},
                new SelectListItem{ Text="Close", Value = "Close"},
                new SelectListItem {Text="Suspended", Value="Suspended" }
            };
            ViewBag.ddlStatusList = list;
        }
        public ActionResult UpdateProjectStatus(long ProjectId = 0)
        {
            PopulateStatusList();
            ViewBag.Class = "display-hide";
            if (ProjectId > 0)
            {
                UpdateProjectStatusModel model = new UpdateProjectStatusModel() { ProjectId = ProjectId };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProjectStatus(UpdateProjectStatusModel model)
        {
            PopulateStatusList();
            ViewBag.Class = "alert-danger";
            ViewBag.Message = "Error Occurred";
            try
            {
                if (ModelState.IsValid)
                {
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var Clientresult = client.PostAsync("/api/Project/UpdateProjectStatus", content).Result;
                    if (Clientresult.StatusCode == HttpStatusCode.OK)
                    {
                        ViewBag.Class = "alert-success";
                        ViewBag.Message = "Updated Successfully";
                        var contents = Clientresult.Content.ReadAsStringAsync().Result;
                        var response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                        ModelState.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        public ActionResult PaymentRelease(long ProfileID = 0)
        {
            ViewBag.Class = "display-hide";
            ViewBag.listProjects = GetProjectList();
            if (ProfileID > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var Clientresult = client.GetAsync("/api/Project/GetProjectPaymentById/"+ ProfileID).Result;
                if (Clientresult.StatusCode == HttpStatusCode.OK)
                {
                    var contents = Clientresult.Content.ReadAsStringAsync().Result;
                    var response = new JavaScriptSerializer().Deserialize<GetPaymentModel>(contents);
                    AddUpdatePaymentReleaseModel model = new AddUpdatePaymentReleaseModel() { NextDueDate = response.PaymentDueDate, ProjectId = response.ProjectId, PaymentId = response.PaymentId, ReleasedAmount = response.ReleasedAmount };
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult PaymentRelease(AddUpdatePaymentReleaseModel model)
        {
            ViewBag.Class = "display-hide";
            ViewBag.listProjects = GetProjectList();
            try
            {
                if(ModelState.IsValid)
                {
                    model.CreatedBy = UserManager.user.UserId;
                    var dt = DateTime.ParseExact(model.NextDueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    HttpResponseMessage Clientresult = null;
                    if (model.PaymentId > 0)
                    {
                        Clientresult = client.PostAsync("/api/Project/UpdatePaymentRelease", content).Result;
                    }
                    else
                    {
                        Clientresult = client.PostAsync("/api/Project/AddPaymentRelease", content).Result;
                    }
                    if (Clientresult.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = Clientresult.Content.ReadAsStringAsync().Result;
                        var response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                        if (response.isSuccess)
                        {
                            ViewBag.Class = "alert-success";
                            ViewBag.Message = "Updated Successfully";
                            ModelState.Clear();
                            return View();
                        }
                    }
                }
                ViewBag.Class = "alert-danger";
                ViewBag.Message = "Not Valid";
            }
            catch (Exception ex)
            {
                ViewBag.Class = "alert-danger";
                ViewBag.Message = ex.Message;
            }
            return View();
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

        private void GetProjectTypeAndProjects()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync("/api/Client/GetClients").Result;
            var ProjectTypeResult = client.GetAsync("/api/Project/GetProjectType").Result;
            if (ProjectTypeResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectTypeResult.Content.ReadAsStringAsync().Result;
                var response = new JavaScriptSerializer().Deserialize<List<ProjectTypeModel>>(contents);
                ViewBag.ProjectType = response;
            }
            ViewBag.listProjects = GetProjectList();
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
            var listProjects = GetProjectList();
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            ProjectWorkingHoursBeforePMSModel objModel = new ProjectWorkingHoursBeforePMSModel();
            ViewBag.Class = "display-hide";
            return View(objModel);
        }

        [HttpPost]
        public ActionResult AddWorkingHoursOfProjectBeforePMS(ProjectWorkingHoursBeforePMSModel model)
        {
            var listProjects = GetProjectList();
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
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
            var listProjects = GetProjectList();
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
            var listProjects = GetProjectList();
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            return View(model);
        }
        [HttpGet]
        public ActionResult MergeProject()
        {
            ViewBag.Class = "display-hide";
            var listProjects = GetProjectList();
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

        public ActionResult ProjectAddendumDetails(long ProjectId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync("/api/Project/GetProjectAddendumDetails?ProjectId=" + ProjectId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<List<GetAddendumModel>>(contents);
                return View(Response);
            }
            return View();
        }


        #region User Defined Functions
        public List<ProjectModel> GetProjectList()
        {
            List<ProjectModel> listProjects = new List<ProjectModel>();
            var client1 = new HttpClient();
            client1.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var ProjectResult = client1.GetAsync("/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectModel>>(contents);
                listProjects = response;
            }
            return listProjects;
        }
        #endregion
    }
}