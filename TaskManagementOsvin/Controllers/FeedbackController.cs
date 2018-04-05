using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class FeedbackController : Controller
    {
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        public ActionResult Feedback()
        {
            List<FeedTypeDomainModel> listFeedTypes = new List<FeedTypeDomainModel>();
            List<ProjectDomainModel> listProjects = new List<ProjectDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync(BaseURL + "/api/Feedback/GetAllFeedtypes").Result;
            if (Clientresult.StatusCode == HttpStatusCode.OK)
            {
                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FeedTypeDomainModel>>(contents);
                listFeedTypes = response;
            }
            var ProjectResult = client.GetAsync(BaseURL + "/api/Project/GetProjectList").Result;
            if (ProjectResult.StatusCode == HttpStatusCode.OK)
            {
                var contents = ProjectResult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjectDomainModel>>(contents);
                listProjects = response;
                listProjects = listProjects.Where(p => p.ProjectId != 11).ToList();
            }
            ViewBag.listFeedTypes = new SelectList(listFeedTypes, "FeedTypeId", "FeedType");
            ViewBag.listProjects = new SelectList(listProjects, "ProjectId", "ProjectTitle");
            ViewBag.Class = "display-hide";
            return View();
        }
        public ActionResult _Feedback()
        {
            List<FeedbackDomainModel> listFeedback = new List<FeedbackDomainModel>();
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync(BaseURL + "/api/Feedback/GetFeedList").Result;
            if (Clientresult.StatusCode == HttpStatusCode.OK)
            {
                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FeedbackDomainModel>>(contents);
                listFeedback = response;
            }
            return PartialView(listFeedback);
        }
        [HttpPost]
        public ActionResult AddUpdateFeedback(FeedbackDomainModel model)
        {
            if(model!=null)
            {
                model.UserId = UserManager.user.UserId;  
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync(BaseURL + "/api/Feedback/AddUpdateFeedback", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Feedback");
        }
        [HttpGet]
        public ActionResult DeleteFeedback(long FeedbackId)
        {
            if (FeedbackId>0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Feedback/DeleteFeedback?FeedbackId="+FeedbackId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Feedback");
        }
        [HttpGet]
        public ActionResult ActivateDeactivateFeedback(long FeedbackId, bool IsActive)
        {
            if (FeedbackId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Feedback/ActivateDeactivateFeedback?FeedbackId=" + FeedbackId + "&IsActive=" + IsActive).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Feedback");
        }
    }
}