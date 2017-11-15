using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementOsvin.Controllers
{
    public class ManagementController : Controller
    {
        public ActionResult AddEmployee(int DealId=0)
        {
            return View();
        }
        // GET: Management
        public ActionResult AddUpdateUser()
        {
            return View();
        }
    }
}