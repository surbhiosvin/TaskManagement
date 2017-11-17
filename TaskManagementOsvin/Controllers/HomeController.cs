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

namespace TaskManagementOsvin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login(string ReturnUrl = "")
        {
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
                        if (ReturnUrl !="")
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
    }
}
