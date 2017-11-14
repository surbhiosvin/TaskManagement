using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagementOsvin.Models;
using Providers.Providers.SP.Repositories;
using DomainModel.EntityModel;
using System.Text.RegularExpressions;

namespace TaskManagementOsvin.Controllers
{
    public class EmployeeController : ApiController
    {
        static readonly IEmployee EmployeeRepository = new EmployeeRepository();
        // GET api/<controller>
        [HttpPost]
        [Route("~/api/Employee/AuthenticateUser")]
        public HttpResponseMessage AuthenticateUser(User model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    UserDetails user = new UserDetails() { Email = model.email, Password = model.password };
                    var Employee = EmployeeRepository.AuthenticateEmployees(user);
                    if (Employee != null)
                    {
                        roleType GetRoleType;
                        var roleType = Regex.Replace(Employee.Role, @"\s+", "");
                        Enum.TryParse(roleType, out GetRoleType);
                        Employee.roleType = GetRoleType;
                        Employee.isSuccess = true;
                        Employee.response = "Success";
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, Employee);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, model);
                    }
                    return httpResponse;
                }
                else
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    return httpResponse;
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.", StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpPost]
        [Route("~/api/Employee/ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePasswordReqModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    var Response = EmployeeRepository.ChangePassword(model);
                    if (Response.isSuccess)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, Response);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, Response);
                    }
                }
                else
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
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