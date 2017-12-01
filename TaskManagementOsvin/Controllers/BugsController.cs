using DomainModel.EntityModel;
using Newtonsoft.Json;
using Providers.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskManagementOsvin.Models;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    public class BugsController : Controller
    {
        // GET: Bugs
        public ActionResult ReportBug(long BugId=0)
        {
            var client = new HttpClient();
            BugsModel model = new BugsModel();
            if (BugId > 0)
            {
                model.BugId = BugId;
                model.Mode = "Edit";
                model.UserId = 0;
                model.ProjectId = 0;
                var serialized = JsonConvert.SerializeObject(model);
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync("/api/Bugs/GetBugDetails", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = JsonConvert.DeserializeObject<List<BugsModel>>(contents);
                    if(Response.Count>0)
                    model = Response.FirstOrDefault();
                }
            }
            client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync("/api/Bugs/GetEmployees").Result;
            if (Clientresult.StatusCode == HttpStatusCode.OK)
            {
                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                var response = new JavaScriptSerializer().Deserialize<List<EmployeesDomainModel>>(contents);
                model.listEmployees = response;
            }
            var ClientResult1 = client.GetAsync("/api/Project/GetProjectList").Result;
            if (ClientResult1.StatusCode == HttpStatusCode.OK)
            {
                var contents = ClientResult1.Content.ReadAsStringAsync().Result;
                var response = new JavaScriptSerializer().Deserialize<List<ProjectDomainModel>>(contents);
                model.listProjects = response;
            }
            var listPriority = new List<SelectListItem>
            {
                new SelectListItem{ Text="High", Value = "High"},
                new SelectListItem{ Text="Low", Value = "Low"},
                new SelectListItem{ Text="Urgent", Value = "Urgent"},
                new SelectListItem {Text="Immediate", Value="Immediate" }
            };
            var listStatus = new List<SelectListItem>
            {
                new SelectListItem{ Text="New", Value = "New"},
                new SelectListItem{ Text="ReOpened", Value = "ReOpened"},
                new SelectListItem {Text="Closed", Value="Closed" }
            };
            ViewData["ddlPriority"] = listPriority;
            ViewData["ddlStatus"] = listStatus;            
            ViewBag.Class = "display-hide";
            return View(model);
        }
        [HttpPost]
        public ActionResult ReportBug(BugsModel model)
        {
            ViewBag.Class = "display-hide";
            var client = new HttpClient();
            try
            {
                if (ModelState.IsValid)
                {
                    model.Files = "";
                    model.BugReportedBy = UserManager.user.UserId;
                    HttpPostedFileBase[] PostedFiles = model.PostedFiles;
                    model.PostedFiles = null;
                    var serialized = JsonConvert.SerializeObject(model);
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/Bugs/AddUpdateBug", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        if (Response.isSuccess == true)
                        {
                            if (Response.response == "Bug added successfully.")
                            { 
                                //String Email = GetUserEmail(model.UserId);                                
                                ////Session["Email1"] = Email;
                                ////Session["ProjectName"] = model.ProjectId;
                                ////Session["Bugsubject"] = model.BugSubject;
                                //Thread threadSendMails;

                                //threadSendMails = new Thread(delegate ()
                                //{
                                //    //SendEmail(Session["Email1"].ToString(), Session["ProjectName"].ToString(), Session["Bugsubject"].ToString());
                                //    SendEmail(Email, GetProjectTitle(model.ProjectId), model.BugSubject.ToString());

                                //});
                                //threadSendMails.IsBackground = true;

                                //threadSendMails.Start();

                                string insertfile = string.Empty;
                                foreach (HttpPostedFileBase file in PostedFiles)
                                {
                                    if (file != null)
                                    {
                                        Guid g1 = Guid.NewGuid();
                                        string ext = System.IO.Path.GetExtension(file.FileName);
                                        string fileName = g1 + ext;
                                        var ServerSavePath = Path.Combine(Server.MapPath("~/BugUploadDocument/") + fileName);
                                        file.SaveAs(ServerSavePath);
                                        insertfile = AddBugFiles(fileName);
                                    }
                                }
                                if (insertfile != null)
                                {
                                    if (insertfile == "InsertImage")
                                    {
                                        ModelState.Clear();
                                        ModelState.AddModelError("CustomError", Response.response);
                                        ViewBag.Class = "alert-success";
                                        model = new BugsModel();
                                    }
                                }
                            }
                            else if (Response.response == "Bug updated successfully.")
                            {
                                //if (model.Status == "ReOpened")
                                //{
                                //    String Email = GetUserEmail(model.UserId);
                                //    //Session["Email1"] = Email;
                                //    //Session["ProjectName"] = model.ProjectId;
                                //    //Session["Bugsubject"] = Convert.ToString(model.BugSubject);
                                //    Thread threadSendMails;

                                //    threadSendMails = new Thread(delegate ()
                                //    {
                                //        //SendEmail(Session["Email1"].ToString(), Session["ProjectName"].ToString(), Session["Bugsubject"].ToString());
                                //        SendEmail(Email, GetProjectTitle(model.ProjectId), model.BugSubject);
                                //    });
                                //    threadSendMails.IsBackground = true;

                                //    threadSendMails.Start();
                                //}
                                string updatefile = string.Empty;
                                foreach (HttpPostedFileBase file in PostedFiles)
                                {
                                    if (file != null)
                                    {
                                        Guid g1 = Guid.NewGuid();
                                        string ext = System.IO.Path.GetExtension(file.FileName);
                                        string fileName = g1 + ext;
                                        var ServerSavePath = Path.Combine(Server.MapPath("~/BugUploadDocument/") + fileName);
                                        file.SaveAs(ServerSavePath);
                                        updatefile = UpdateBugFiles(model.BugId, fileName);
                                    }
                                }
                                if (updatefile != null)
                                {
                                    if (updatefile == "UpdateImage")
                                    {
                                        ModelState.Clear();
                                        ModelState.AddModelError("CustomError", Response.response);
                                        ViewBag.Class = "alert-success";
                                        model = new BugsModel();
                                    }
                                }
                            }
                        }
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.Class = "alert-danger";
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "Error occurred");
                        ViewBag.Class = "alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.Class = "alert-danger";
            }
            client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync("/api/Bugs/GetEmployees").Result;
            if (Clientresult.StatusCode == HttpStatusCode.OK)
            {
                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                var response = new JavaScriptSerializer().Deserialize<List<EmployeesDomainModel>>(contents);
                model.listEmployees = response;
            }
            var ClientResult1 = client.GetAsync("/api/Project/GetProjectList").Result;
            if (ClientResult1.StatusCode == HttpStatusCode.OK)
            {
                var contents = ClientResult1.Content.ReadAsStringAsync().Result;
                var response = new JavaScriptSerializer().Deserialize<List<ProjectDomainModel>>(contents);
                model.listProjects = response;
            }
            var listPriority = new List<SelectListItem>
            {
                new SelectListItem{ Text="High", Value = "High"},
                new SelectListItem{ Text="Low", Value = "Low"},
                new SelectListItem{ Text="Urgent", Value = "Urgent"},
                new SelectListItem {Text="Immediate", Value="Immediate" }
            };
            var listStatus = new List<SelectListItem>
            {
                new SelectListItem{ Text="New", Value = "New"},
                new SelectListItem{ Text="ReOpened", Value = "ReOpened"},
                new SelectListItem {Text="Closed", Value="Closed" }
            };
            ViewData["ddlPriority"] = listPriority;
            ViewData["ddlStatus"] = listStatus;
            return View(model);
        }
        #region UserDefined Functions
        public string AddBugFiles(string Files)
        {
            BugFilesDomainModel model = new BugFilesDomainModel() { Files = Files };
            string Response = string.Empty;
            var client = new HttpClient();
            var serialized = JsonConvert.SerializeObject(model);
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync("/api/Bugs/AddBugFiles", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                Response = JsonConvert.DeserializeObject<string>(contents);
            }
            return Response;
        }
        public string UpdateBugFiles(long BugId, string Files)
        {
            BugFilesDomainModel model = new BugFilesDomainModel() {BugId=BugId, Files = Files };
            string Response = string.Empty;
            var client = new HttpClient();
            var serialized = JsonConvert.SerializeObject(model);
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync("/api/Bugs/UpdateBugFiles",content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                Response = JsonConvert.DeserializeObject<string>(contents);
            }
            return Response;
        }
        public string GetUserEmail(long UserId)
        {
            string Email = string.Empty;
            try
            {
                if (UserId > 0)
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.GetAsync("/api/Management/GetEmployeeDataById?UserId=" + UserId).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<EmployeeDomainModel>(contents);
                        if(Response!=null && Response.UserId>0)
                        {
                            Email = Response.Email;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return Email;
        }
        public string GetProjectTitle(long ProjectId)
        {
            string ProjectName = string.Empty;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync("/api/Project/GetProjectFullDetails?ProjectId=" + ProjectId).Result;
                if(result.StatusCode==HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = JsonConvert.DeserializeObject<ProjectFullDetailsDomainModel>(contents);
                    if (Response != null && Response.ProjectId > 0)
                    {
                        ProjectName = Response.ProjectTitle;
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog.LogError(ex);
            }
            return ProjectName;
        }
        #endregion
    }
}