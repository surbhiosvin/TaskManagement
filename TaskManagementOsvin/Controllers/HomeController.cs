using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskManagementOsvin.Models;
using System.Text;
using System.Net;
using DomainModel.EntityModel;

namespace TaskManagementOsvin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.Class = "display-hide";
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/Employee/AuthenticateUser", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var getUser = new JavaScriptSerializer().Deserialize<UserDetails>(contents);
                        return RedirectToAction("Welcome", "Dashboard");
                    }
                    else if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        ViewBag.Class = null;
                        ModelState.AddModelError("CustomError", "Email/ Password is incorrect");
                    }
                    else
                    {
                        ViewBag.Class = null;
                        ModelState.AddModelError("CustomError", "Error occurred");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Class = null;
                ModelState.AddModelError("CustomError", ex.Message);
            }
            return View();
        }
    }
}
