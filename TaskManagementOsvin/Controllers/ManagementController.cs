using DomainModel.EntityModel;
using Providers.Helper;
using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TaskManagementOsvin.Models;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    public class ManagementController : Controller
    {
        static readonly IManagement managementRepository = new ManagementRepository();
        [HttpGet]
        public ActionResult AddEmployee(int UserId = 0)
        {
            EmployeeModel employee = new EmployeeModel();
            if (UserId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync("/api/Management/GetEmployeeDataById?UserId=" + UserId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<EmployeeModel>(contents);
                    employee = Response;
                    employee.DOB = employee.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            employee.listDepartment = GetDepartments();
            employee.listDepartment = employee.listDepartment.Where(s => s.IsActive == true).ToList();
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Admin", Value = "Admin"},
                new SelectListItem{ Text="HR", Value = "HR"},
                new SelectListItem{ Text="Project Manager", Value = "Project Manager"},
                new SelectListItem{ Text="Network Admin", Value = "Network Admin"},
                new SelectListItem{ Text="Team Leader", Value = "Team Leader"},
                new SelectListItem {Text="Employee", Value="Employee" }
            };
            if (employee.DepartmentId > 0)
            {
                employee.listDesignation = GetDesignations(employee.DepartmentId);
            }
            ViewData["ddlRole"] = list;
            ViewBag.Class = "display-hide";
            return View(employee);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = UserManager.user.UserId;
                    model.Archived = "NonArchive";
                    string path = Server.MapPath("~/UploadDocuments/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (model.ImageFile != null)
                    {
                        string Imagepath = Server.MapPath("~/Images/UserImages/");
                        if (!Directory.Exists(Imagepath))
                        {
                            Directory.CreateDirectory(Imagepath);
                        }
                        string fileName = Path.GetFileName(model.ImageFile.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                        model.ImageFile.SaveAs(Imagepath + fileName);
                        model.Image = fileName;
                        model.ImageFile = null;
                    }
                    if (model.IDCardFirst_File != null)
                    {
                        string fileName = Path.GetFileName(model.IDCardFirst_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.IDCardFirst_File.FileName);
                        model.IDCardFirst_File.SaveAs(path + fileName);
                        model.IDCardFirst = fileName;
                        model.IDCardFirst_File = null;
                    }
                    if (model.IDCardSecond_AddressProof_File != null)
                    {
                        string fileName = Path.GetFileName(model.IDCardSecond_AddressProof_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.IDCardSecond_AddressProof_File.FileName);
                        model.IDCardSecond_AddressProof_File.SaveAs(path + fileName);
                        model.IDCardSecond_AddressProof_ = fileName;
                        model.IDCardSecond_AddressProof_File = null;
                    }
                    if (model.CV_File != null)
                    {
                        string fileName = Path.GetFileName(model.CV_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.CV_File.FileName);
                        model.CV_File.SaveAs(path + fileName);
                        model.CV = fileName;
                        model.CV_File = null;
                    }
                    if (model.ExperienceCertificate_File != null)
                    {
                        string fileName = Path.GetFileName(model.ExperienceCertificate_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.ExperienceCertificate_File.FileName);
                        model.ExperienceCertificate_File.SaveAs(path + fileName);
                        model.ExperienceCertificate = fileName;
                        model.ExperienceCertificate_File = null;
                    }
                    if (model.AppointementLetter_File != null)
                    {
                        string fileName = Path.GetFileName(model.AppointementLetter_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.AppointementLetter_File.FileName);
                        model.AppointementLetter_File.SaveAs(path + fileName);
                        model.AppointementLetter = fileName;
                        model.AppointementLetter_File = null;
                    }
                    if (model.AggrementDocument_File != null)
                    {
                        string fileName = Path.GetFileName(model.AggrementDocument_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.AggrementDocument_File.FileName);
                        model.AggrementDocument_File.SaveAs(path + fileName);
                        model.AggrementDocument = fileName;
                        model.AggrementDocument_File = null;
                    }
                    if (model.OtherDocument_File != null)
                    {
                        string fileName = Path.GetFileName(model.OtherDocument_File.FileName);
                        fileName = "test" + Guid.NewGuid().ToString() + Path.GetExtension(model.OtherDocument_File.FileName);
                        model.OtherDocument_File.SaveAs(path + fileName);
                        model.OtherDocument = fileName;
                        model.OtherDocument_File = null;
                    }
                    if (!string.IsNullOrWhiteSpace(model.DOB))
                       model.DateOfBirth= DateTime.ParseExact(model.DOB, "dd/MM/yyyy", null);
                    //model.DateOfBirth = Convert.ToDateTime(model.DOB);
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync("/api/Management/AddUpdateEmployee", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                        ModelState.Clear();
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.AlertType = "alert-success";
                        ViewBag.Class = null;
                        model = new EmployeeModel();
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
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
            model.listDepartment = GetDepartments();
            model.listDepartment = model.listDepartment.Where(s => s.IsActive == true).ToList();
            if (model.DepartmentId > 0)
            {
                model.listDesignation = GetDesignations(model.DepartmentId);
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Admin", Value = "Admin"},
                new SelectListItem{ Text="HR", Value = "HR"},
                new SelectListItem{ Text="Project Manager", Value = "Project Manager"},
                new SelectListItem{ Text="Network Admin", Value = "Network Admin"},
                new SelectListItem{ Text="Team Leader", Value = "Team Leader"},
                new SelectListItem {Text="Employee", Value="Employee" }
            };
            ViewData["ddlRole"] = list;
            return View(model);
        }
        // GET: Management

        public ActionResult AddUpdateUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _AddUpdateUser(bool Archived = false)
        {
            UserListSearchModel model = new UserListSearchModel() { UserId = UserManager.user.UserId, Role = UserManager.user.Role, Name = "", Archived = Archived == true ? "Archive" : "NonArchive" };
            List<EmployeeDomainModel> listusers = new List<EmployeeDomainModel>();
            var serialized = new JavaScriptSerializer().Serialize(model);
            var client = new HttpClient();
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync("/api/Management/UserListBySearch", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<UserListDomainModel>(contents);
                listusers = Response.listUsers;
            }
            return PartialView(listusers);
        }

        [HttpGet]
        public ActionResult Departments()
        {
            ViewBag.Class = "display-hide";
            List<DepartmentDomainModel> listDepartments = new List<DepartmentDomainModel>();
            listDepartments = GetDepartments();
            return View(listDepartments);
        }        
        [HttpGet]
        public ActionResult EmployeeDetails(long UserId)
        {
            EmployeeModel employee = new EmployeeModel();
            if (UserId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync("/api/Management/GetEmployeeDataById?UserId=" + UserId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<EmployeeModel>(contents);
                    employee = Response;
                    employee.DOB = employee.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            return View(employee);
        }
        [HttpGet]
        public ActionResult ArchiveEmployee(long UserId)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync("/api/Management/UpdateEmployeeArchive?UserId="+UserId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                objRes = Response;
            }
            return RedirectToAction("AddUpdateUser");
        }

        [HttpGet]
        public ActionResult Designations()
        {
            ViewBag.Class = "display-hide";
            List<DesignationDomainModel> listDesignations = new List<DesignationDomainModel>();
            listDesignations = GetDesignations();
            return View(listDepartments);
        }
        #region User Defined Functions
        public List<DepartmentDomainModel> GetDepartments()
        {
            List<DepartmentDomainModel> listDepartments = new List<DepartmentDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync("/api/Management/GetAllDepartments").Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<DepartmentDomainModel>(contents);
                listDepartments = Response.listDepartments;
            }
            return listDepartments;
        }
        public List<DesignationDomainModel> GetDesignations(long DepartmentId)
        {
            List<DesignationDomainModel> listDesignaton = new List<DesignationDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync("/api/Management/GetDesignationByDeptId?DepartmentId=" + DepartmentId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<DesignationDomainModel>(contents);
                listDesignaton = Response.listDesignations;
            }
            return listDesignaton;
        }
        #endregion
    }
}