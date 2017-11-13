using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagementOsvin.Models;

namespace TaskManagementOsvin.Controllers
{
    public class EmployeeController : ApiController
    {
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
                    //model.isSuccess = true;
                    //model.response = "Success";
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, model);
                    return httpResponse;
                }
                else
                {
                    //model.isSuccess = false;
                    //model.response = "Failure";
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
    }
}