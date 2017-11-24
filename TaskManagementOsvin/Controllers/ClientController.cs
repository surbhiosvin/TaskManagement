using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TaskManagementOsvin.Controllers
{
    public class ClientController : Controller
    {
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
            var Clientresult = client.GetAsync("/api/Client/GetAllClients?Archived="+ (Archived == true ? "Archive" : "NonArchive")).Result;
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
            var result = client.GetAsync("/api/Client/UpdateClientArchive?ClientId=" + ClientId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                objRes = Response;
            }
            return RedirectToAction("_Clients", new {Archived=Archived });
        }
    }
}