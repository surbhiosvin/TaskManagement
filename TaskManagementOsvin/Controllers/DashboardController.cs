using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementOsvin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Welcome()
        {
            return View();
        }
    }
}