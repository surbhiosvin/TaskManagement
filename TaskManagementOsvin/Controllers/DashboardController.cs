using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}