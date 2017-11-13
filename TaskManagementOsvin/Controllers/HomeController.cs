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
using TaskManagement.Business.Abstract;
using TaskManagement.Business.Common;
using TaskManagement.Business.Concrete;
using TaskManagement.DataAccess.Domain;

namespace TaskManagementOsvin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userservice = new UserService();
        public ActionResult Login()
        {
            ViewBag.Class = "display-hide";
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            Result _result = new Result();
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    _result = userservice.Login(model.email, model.password);
                    if (_result.IsSuccessfull == true)
                    {
                        tblUser user = (tblUser)_result.Data;
                        string EmployeeRole = user.Role;
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
//if (ModelState.IsValid)
//{
//    var serialized = new JavaScriptSerializer().Serialize(model);
//    var client = new HttpClient();
//    var content = new StringContent(serialized, Encoding.UTF8, "application/json");
//    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
//    var result = client.PostAsync("/api/Employee/AuthenticateUser", content).Result;
//    if (result.StatusCode == HttpStatusCode.OK)
//    {
//        var contents = result.Content.ReadAsStringAsync().Result;
//        var getUser = new JavaScriptSerializer().Deserialize<User>(contents);
//        return RedirectToAction("Welcome", "Dashboard");
//    }
//    else if(result.StatusCode == HttpStatusCode.NotFound)
//    {
//        ViewBag.Class = null;
//        ModelState.AddModelError("CustomError", "Email/ Password is incorrect");
//    }
//    else
//    {
//        ViewBag.Class = null;
//        ModelState.AddModelError("CustomError", "Error occurred");
//    }
//}