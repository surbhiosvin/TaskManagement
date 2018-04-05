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
using TaskManagementOsvin.Security;
using System.Configuration;

namespace TaskManagementOsvin.Controllers
{
    public class HomeController : Controller
    {
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        public ActionResult Login(string ReturnUrl = "")
        {
            if (UserManager.user != null && UserManager.user.UserId > 0)
            {
                return RedirectToAction("Welcome", "Dashboard");
            }            
            ViewBag.Class = "display-hide";
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model, string ReturnUrl = "")
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    model.url = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = UserManager.validateuser(model);
                    if (result == HttpStatusCode.OK)
                    {
                        if (ReturnUrl != "")
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Welcome", "Dashboard");
                    }
                    else if (result == HttpStatusCode.NotFound || result == HttpStatusCode.Unauthorized)
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

        [ChildActionOnly]
        public ActionResult _notifications()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Notification/ProfileNotifications").Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var notifications = new JavaScriptSerializer().Deserialize<List<EmployeeModel>>(contents);
                return PartialView(notifications);
            }
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _birthdayNotifications()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Notification/BirthDayNotifications").Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var notifications = new JavaScriptSerializer().Deserialize<List<BirthDayNotificationModel>>(contents);
                return PartialView(notifications);
            }
            return PartialView();
        }

        public ActionResult SignOut()
        {
            UserManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}
