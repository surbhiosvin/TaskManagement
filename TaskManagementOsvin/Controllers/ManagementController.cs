using DomainModel.EntityModel;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Providers.Helper;
using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using TaskManagementOsvin.Models;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        static readonly IManagement managementRepository = new ManagementRepository();
        [HttpGet]
        public ActionResult AddEmployee(int UserId = 0)
        {
            EmployeeModel employee = new EmployeeModel();
            if (UserId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/GetEmployeeDataById?UserId=" + UserId).Result;
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
                        model.DateOfBirth = DateTime.ParseExact(model.DOB, "dd/MM/yyyy", null);
                    //model.DateOfBirth = Convert.ToDateTime(model.DOB);
                    var serialized = JsonConvert.SerializeObject(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync(BaseURL + "/api/Management/AddUpdateEmployee", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.Clear();
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.Class = "alert-success";
                        model = new EmployeeModel();
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = JsonConvert.DeserializeObject<ResponseModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.Class = "alert-danger";
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "Error occurred");
                        ViewBag.Class = "alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.Class = "alert-danger";
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
        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        public ActionResult AddUpdateUser()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _AddUpdateUser(bool Archived = false)
        {
            ViewBag.Class = "display-hide";
            UserListSearchModel model = new UserListSearchModel() { UserId = UserManager.user.UserId, Role = UserManager.user.Role, Name = "", Archived = Archived == true ? "Archive" : "NonArchive" };
            List<EmployeeDomainModel> listusers = new List<EmployeeDomainModel>();
            var serialized = new JavaScriptSerializer().Serialize(model);
            var client = new HttpClient();
            var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.PostAsync(BaseURL + "/api/Management/UserListBySearch", content).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<UserListDomainModel>(contents);
                listusers = Response.listUsers;
                if (listusers != null && listusers.Count > 0)
                {
                    foreach (var Emp in listusers)
                    {
                        Emp.DOB = Convert.ToDateTime(Emp.DateOfBirth).ToString("d MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                }
            }
            return PartialView(listusers);
        }

        [HttpGet]
        [CustomAuthorize(roles: "HR,Admin")]
        public ActionResult Departments()
        {
            ViewBag.Class = "display-hide";
            return View();
        }
        [HttpGet]
        public ActionResult _Departments()
        {
            List<DepartmentDomainModel> listDepartments = new List<DepartmentDomainModel>();
            listDepartments = GetDepartments();
            return PartialView(listDepartments);
        }
        [HttpGet]
        public ActionResult EmployeeDetails(long UserId)
        {
            EmployeeModel employee = new EmployeeModel();
            if (UserId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/GetEmployeeDataById?UserId=" + UserId).Result;
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
        public ActionResult ArchiveEmployee(long UserId, bool Archived = false)
        {
            ResponseDomainModel objRes = new ResponseDomainModel();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Management/UpdateEmployeeArchive?UserId=" + UserId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<ResponseDomainModel>(contents);
                objRes = Response;
            }
            return RedirectToAction("_AddUpdateUser", new { Archived = Archived });
        }

        [HttpGet]
        [CustomAuthorize(roles: "HR,Admin,Team Leader,Project Manager")]
        public ActionResult Designations()
        {
            ViewBag.Class = "display-hide";
            ViewBag.listDepartments = GetDepartments();
            return View();
        }
        [HttpGet]
        public ActionResult _Designations()
        {
            ViewBag.Class = "display-hide";
            List<DesignationDomainModel> listDesignations = new List<DesignationDomainModel>();
            if (UserManager.user.roleType == roleTypeModel.TeamLeader)
            {
                listDesignations = GetDesignationsBasedOnRole(UserManager.user.DepartmentId);
            }
            else
            {
                listDesignations = GetDesignationsBasedOnRole(0);
            }
            return PartialView(listDesignations);
        }
        [HttpGet]
        public ActionResult GenerateSalarySlip(long UserId = 0)
        {
            PaySlipModel model = new PaySlipModel();
            if (UserId > 0)
            {
                model = (PaySlipModel)Session["PaySlip"];
                if (model == null)
                {
                    model = new PaySlipModel();
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.GetAsync(BaseURL + "/api/Management/GetEmployeeDataById?UserId=" + UserId).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<EmployeeModel>(contents);
                        model.EmployeeID = Response.UserId;
                        model.DateOfJoining = Response.DateOfJoining;
                        model.DepartmentId = Response.DepartmentId;
                        model.DesignationId = Response.DesignationId;
                    }
                }
            }
            model.listEmployees = GetEmployeeList();
            model.listDepartments = GetDepartments();
            if (model.DepartmentId > 0)
            {
                model.listDesignations = GetDesignations(model.DepartmentId);
            }
            ViewBag.Class = "display-hide";
            return View(model);
        }
        [HttpPost]
        public ActionResult GenerateSalarySlip(PaySlipModel model)
        {
            ViewBag.Class = "display-hide";
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = UserManager.user.UserId;
                    string month = model.PaySlipMonth.Substring(0, 2);
                    model.PaySlipMonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt32(month));
                    model.PaySlipYear = model.PaySlipMonth.Substring(3, 4);
                    model.PaySlipMonth = model.PaySlipMonthName + " " + model.PaySlipYear;
                    //model.DateOfBirth = Convert.ToDateTime(model.DOB);
                    var serialized = new JavaScriptSerializer().Serialize(model);
                    var client = new HttpClient();
                    var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                    var result = client.PostAsync(BaseURL + "/api/Management/AddUpdateEmployeePaySlip", content).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<PaySlipModel>(contents);
                        //ModelState.Clear();
                        //ModelState.AddModelError("CustomError", Response.response);
                        //ViewBag.AlertType = "alert-success";
                        //ViewBag.Class = null;
                        //model = new PaySlipModel();                     
                        Session["PaySlip"] = Response;
                        return View("PaySlip", Response);
                    }
                    else if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var contents = result.Content.ReadAsStringAsync().Result;
                        var Response = new JavaScriptSerializer().Deserialize<PaySlipModel>(contents);
                        ModelState.AddModelError("CustomError", Response.response);
                        ViewBag.Class = "alert-danger";
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "Error occurred");
                        ViewBag.Class = "alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                ViewBag.Class = "alert-danger";
            }
            model.listEmployees = GetEmployeeList();
            model.listDepartments = GetDepartments();
            model.listDepartments = model.listDepartments.Where(s => s.IsActive == true).ToList();
            if (model.DepartmentId > 0)
            {
                model.listDesignations = GetDesignations(model.DepartmentId);
            }
            return View(model);
        }
        [HttpGet]
        public void GeneratePaySlipPdf()
        {
            var model = (PaySlipModel)Session["PaySlip"];
            string HTMLContent = ConvertViewToString("_PaySlip", model);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringBuilder sb = new StringBuilder();
            sb.Append(HTMLContent);
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 5f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        public ActionResult EmailPaySlipToEmployee()
        {
            var model = (PaySlipModel)Session["PaySlip"];
            string body = ConvertViewToString("_PaySlip", model);
            bool res = Email.SendEmail(model.Email, body, "Pay Slip");
            return View("PaySlip", model);
        }
        [HttpGet]
        public ActionResult DeleteDepartmentById(long DepartmentId)
        {
            if (DepartmentId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/DeleteDepartmentById?DepartmentId=" + DepartmentId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Departments");
        }
        [HttpPost]
        public ActionResult AddUpdateDepartment(DepartmentDomainModel model)
        {
            if (model != null)
            {
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync(BaseURL + "/api/Management/AddupdateDepartment", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Departments");
        }
        [HttpPost]
        public ActionResult AddUpdateDesignation(DesignationDomainModel model)
        {
            if (model != null)
            {
                if (UserManager.user.roleType == roleTypeModel.TeamLeader)
                {
                    model.DepartmentId = UserManager.user.DepartmentId;
                }
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.PostAsync(BaseURL + "/api/Management/AddupdateDesignation", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Designations");
        }
        [HttpGet]
        public ActionResult DeleteDesignationById(long DesignationId)
        {
            if (DesignationId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/DeleteDesignationById?DesignationId=" + DesignationId).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Designations");
        }
        [HttpGet]
        public ActionResult ActivateDeactivateDepartment(long DepartmentId, bool IsActive)
        {
            if (DepartmentId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/ActivateDeactivateDepartment?DepartmentId=" + DepartmentId + "&IsActive=" + IsActive).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Departments");
        }
        [HttpGet]
        public ActionResult ActivateDeactivateDesignation(long DesignationId, bool IsActive)
        {
            if (DesignationId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/ActivateDeactivateDesignation?DesignationId=" + DesignationId + "&IsActive=" + IsActive).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_Designations");
        }
        [HttpGet]
        public ActionResult ActivateDeactivateUser(long UserId, bool IsActive, bool Archived=false)
        {
            if (UserId > 0)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
                var result = client.GetAsync(BaseURL + "/api/Management/ActivateDeactivateUser?UserId=" + UserId + "&IsActive=" + IsActive).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var Response = new JavaScriptSerializer().Deserialize<ResponseModel>(contents);
                }
            }
            return RedirectToAction("_AddUpdateUser",Archived);
        }

        #region User Defined Functions
        public List<DepartmentDomainModel> GetDepartments()
        {
            List<DepartmentDomainModel> listDepartments = new List<DepartmentDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Management/GetAllDepartments").Result;
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
            var result = client.GetAsync(BaseURL + "/api/Management/GetDesignationByDeptId?DepartmentId=" + DepartmentId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<DesignationDomainModel>(contents);
                listDesignaton = Response.listDesignations;
            }
            return listDesignaton;
        }
        public List<DesignationDomainModel> GetDesignationsBasedOnRole(long DepartmentId)
        {
            List<DesignationDomainModel> listDesignaton = new List<DesignationDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Management/GetDesignationsBasedOnRole?DepartmentId=" + DepartmentId).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<DesignationDomainModel>(contents);
                listDesignaton = Response.listDesignations;
            }
            return listDesignaton;
        }
        public List<EmployeeDomainModel> GetEmployeeList()
        {
            List<EmployeeDomainModel> listEmployees = new List<EmployeeDomainModel>();
            var client = new HttpClient();
            client.BaseAddress = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var result = client.GetAsync(BaseURL + "/api/Management/GetEmployeeList").Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var contents = result.Content.ReadAsStringAsync().Result;
                var Response = new JavaScriptSerializer().Deserialize<PaySlipDomainModel>(contents);
                listEmployees = Response.listEmployees;
            }
            return listEmployees;
        }
        public string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        #endregion
    }
}