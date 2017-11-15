using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public HttpResponseMessage AuthenticateUser(UserDetailsDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    var Employee = EmployeeRepository.AuthenticateEmployees(model);
                    if (Employee != null)
                    {
                        roleTypeDomainModel GetRoleType;
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
        public HttpResponseMessage ChangePassword(ChangePassword model)
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

        [HttpPost]
        [Route("~/api/Employee/SummaryOfWeekDetails")]
        public HttpResponseMessage SummaryOfWeekDetails(GetSummaryDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                if (model != null)
                {
                    var Summary = EmployeeRepository.SummaryOfWeekDetailsMain(model);
                    if (Summary != null)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, Summary);
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occurred");
                    }
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, "Not authorized");
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