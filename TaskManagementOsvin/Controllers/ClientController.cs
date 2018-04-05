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
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
    public class ClientController : Controller
    {
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        [HttpGet]
        public ActionResult Clients()
        {
            ViewBag.Class = "display-hide";
            return View();
        }
        [HttpGet]
        public ActionResult _Clients(bool Archived = false)
        {            
            var client = new HttpClient();
            List<ClientDomainModel> listClients = new List<ClientDomainModel>();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var Clientresult = client.GetAsync(BaseURL + "/api/Client/GetAllClients?Archived="+ (Archived == true ? "Archive" : "NonArchive")).Result;
            if (Clientresult.StatusCode == HttpStatusCode.OK)
            {
                var contents = Clientresult.Content.ReadAsStringAsync().Result;
                var response = new JavaScriptSerializer().Deserialize<List<ClientDomainModel>>(contents);
                listClients = response;
            }
            return PartialView(listClients);
        }
        [HttpGet]
        public ActionResult ArchiveClient(long ClientId,bool Archived = false)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Client/UpdateClientArchive?ClientId=" + ClientId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                objRes = Response;
            }
            return RedirectToAction("_Clients", new {Archived=Archived });
        }
        [HttpPost]
        public ActionResult AddUpdateClient(ClientDomainModel model)
        {
            if (model != null)
            {
                if (model.ClientId == 0)
                    model.Archived = "NonArchive";
                model.CreatedBy = UserManager.user.UserId;
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync(BaseURL + "/api/Client/AddUpdateClient", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                }
            }
            return RedirectToAction("_Clients", new { Archived = model.Archived=="NonArchive"?false:true });
        }
    }
}