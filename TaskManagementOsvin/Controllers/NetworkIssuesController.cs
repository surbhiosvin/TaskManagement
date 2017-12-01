using DomainModel.EntityModel;
using Newtonsoft.Json;
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
    public class NetworkIssuesController : Controller
    {
        // GET: NetworkIssues
        public ActionResult ReportIssue()
        {
            ViewBag.Class = "display-hide";            
            return View();
        }
        public ActionResult _ReportIssue(ReportIssueDomainModel model)
        {
            List<ReportIssueDomainModel> listReportIssues = new List<ReportIssueDomainModel>();
            if (model != null)
            {
                model.UserId = UserManager.user.UserId;
                model.Role = UserManager.user.Role;
                if (model.Status == null)
                    model.Status = "";
                if (model.IsRead == null)
                    model.IsRead = "";
                var serialized = JsonConvert.SerializeObject(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync("/api/NetworkIssues/GetReportIssues", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = JsonConvert.DeserializeObject<List<ReportIssueDomainModel>>(contents);
                    listReportIssues = Response;
                }
                if (listReportIssues != null && listReportIssues.Count > 0)
                {
                    ViewBag.TotalCount = listReportIssues[0].TotalCount;
                    ViewBag.ReadCount = listReportIssues[0].ReadCount;
                    ViewBag.UnReadCount = listReportIssues[0].UnReadCount;
                }
                else 
                {
                    var result1 = client.GetAsync("/api/NetworkIssues/GetReportedIssueCount?UserId=" + model.UserId+"&Role="+model.Role).Result;
                    if (result1.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result1.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<List<ReportIssueDomainModel>>(contents);
                        if(Response!=null && Response.Count>0)
                        {
                            ViewBag.TotalCount = Response[0].TotalCount;
                            ViewBag.ReadCount = Response[0].ReadCount;
                            ViewBag.UnReadCount = Response[0].UnReadCount;
                        }
                    }
                }               
            }           
            return PartialView(listReportIssues);
        }
        [HttpPost]
        public ActionResult AddUpdateReportIssue(ReportIssueDomainModel model)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            if (model != null)
            {
                model.UserId = UserManager.user.UserId;   
                var serialized = JsonConvert.SerializeObject(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync("/api/NetworkIssues/AddUpdateReportIssue", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = JsonConvert.DeserializeObject<ResponseDomainModel>(contents);
                    objRes = Response;
                }
            }
            return RedirectToAction("_ReportIssue");
        }
        public ActionResult NetworkIssueReport()
        {
            ViewBag.Class = "display-hide";
            return View();
        }
    }
}