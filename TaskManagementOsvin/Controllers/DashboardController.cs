using DomainModel.EntityModel;
using Providers.Providers.SP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Welcome()
        {
            var id = UserManager.user.UserId;
            var username = User.Identity.Name;
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ViewBag.Class = "display-hide";
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordReqModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    UserDetails user = EmployeeRepository.GetCurrentUserProfile();
                    model.UserId = user != null ? user.UserId : 0;
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/Employee/ChangePassword", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<Response>(contents);
                        ModelState.Clear();
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.AlertType = "alert-success";
                        ViewBag.Class = null;
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<Response>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.Class = null;
                        ViewBag.AlertType = "alert-danger";
                    }
                    else if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        ModelState.AddModelError("CustomError", "Old Password didn't match.");
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
                ViewBag.Class = null;
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.AlertType = "alert-danger";
            }
            return View();
        }

    }
}