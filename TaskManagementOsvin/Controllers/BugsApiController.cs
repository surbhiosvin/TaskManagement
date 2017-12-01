using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainModel.EntityModel;
using Providers.Repositories;
using Providers.Providers.SP.Repositories;

namespace TaskManagementOsvin.Controllers
{
    public class BugsApiController : ApiController
    {
        static readonly IBug bugRepository = new BugRepository();
        [Route("~/api/Bugs/GetEmployees")]
        public HttpResponseMessage GetEmployees()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listEmployees = bugRepository.GetEmployees();
                if (listEmployees == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listEmployees);
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
        [Route("~/api/Bugs/AddUpdateBug")]
        public HttpResponseMessage AddAddUpdateBug(BugsDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = bugRepository.AddUpdateBug(model);
                if (response == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
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
        [Route("~/api/Bugs/AddBugFiles")]
        public HttpResponseMessage AddBugFiles(BugFilesDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = bugRepository.AddBugFiles(model);
                if (response == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
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
        [Route("~/api/Bugs/UpdateBugFiles")]
        public HttpResponseMessage UpdateBugFiles(BugFilesDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = bugRepository.UpdateBugFiles(model);
                if (response == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
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
        [Route("~/api/Bugs/GetBugDetails")]
        public HttpResponseMessage GetBugDetails(BugsDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = bugRepository.GetBugDetails(model);
                if (response == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
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
