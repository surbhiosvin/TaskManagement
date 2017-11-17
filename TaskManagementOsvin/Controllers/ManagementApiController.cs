using DomainModel.EntityModel;
using Providers.Helper;
using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagementOsvin.Models;

namespace TaskManagementOsvin.Controllers
{
    public class ManagementApiController : ApiController
    {
        static readonly IManagement managementRepository = new ManagementRepository();
        // GET api/<controller>
        [HttpGet]
        [Route("~/api/Management/GetAllDepartments")]
        public HttpResponseMessage GetAllDepartments()
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                DepartmentDomainModel objRes = new DepartmentDomainModel();
                objRes.listDepartments = managementRepository.GetDepartments();
                if (objRes.listDepartments != null)
                {
                    objRes.isSuccess = true;
                    objRes.response = "Success";
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/GetDesignationByDeptId")]
        public HttpResponseMessage GetDesignationByDeptId(long DepartmentId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                DesignationDomainModel objRes = new DesignationDomainModel();
                if (DepartmentId > 0)
                {
                    objRes.listDesignations = managementRepository.GetDesignationsByDepartmentId(DepartmentId);
                    if (objRes.listDesignations != null)
                    {
                        objRes.isSuccess = true;
                        objRes.response = "Success";
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                    }
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.NotFound, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpPost]
        [Route("~/api/Management/AddUpdateEmployee")]
        public HttpResponseMessage AddUpdateEmployee(EmployeeDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var res = managementRepository.AddUpdateEmployee(model);
                if (res != null && res.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, res);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpPost]
        [Route("~/api/Management/UserListBySearch")]
        public HttpResponseMessage UserListBySearch(UserListSearchModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                UserListDomainModel objRes = new UserListDomainModel();
                objRes = managementRepository.UserListBySearch(model.UserId, model.Role, model.Archived);
                if (objRes != null && objRes.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/GetEmployeeDataById")]
        public HttpResponseMessage GetEmployeeDataById(long UserId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                EmployeeDomainModel objRes = new EmployeeDomainModel();
                objRes = managementRepository.GetEmployeeDataById(UserId);
                if (objRes != null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/DeleteDepartmentById")]
        public HttpResponseMessage DeleteDepartmentById(long DepartmentId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = managementRepository.DeleteDepartmentById(DepartmentId);
                if (objRes != null && objRes.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/ActivateDeactivateDepartment")]
        public HttpResponseMessage ActivateDeactivateDepartment(long DepartmentId, bool IsActive)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = managementRepository.ActivateDeactivateDepartment(DepartmentId, IsActive);
                if (objRes != null && objRes.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpPost]
        [Route("~/api/Management/AddupdateDepartment")]
        public HttpResponseMessage AddUpdateDepartment(DepartmentDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                ResponseDomainModel res = new ResponseDomainModel();
                if (model != null && !string.IsNullOrWhiteSpace(model.DepartmentName))
                {
                    res = managementRepository.AddUpdateDepartment(model);
                    if (res != null && res.isSuccess)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, res);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                    }
                }
                else
                {
                    res.isSuccess = false;
                    res.response = "Please enter department name";
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/ActivateDeactivateUser")]
        public HttpResponseMessage ActivateDeactivateUser(long UserId, bool IsActive)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = managementRepository.ActivateDeactivateUser(UserId, IsActive);
                if (objRes != null && objRes.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/DeleteUserById")]
        public HttpResponseMessage DeleteUserById(long UserId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = managementRepository.DeleteUserById(UserId);
                if (objRes != null && objRes.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Management/UpdateEmployeeArchive")]
        public HttpResponseMessage UpdateEmployeeArchive(long UserId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = managementRepository.UpdateEmployeeArchive(UserId);
                if (objRes != null && objRes.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, objRes);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
