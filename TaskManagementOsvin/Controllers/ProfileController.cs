using DomainModel.EntityModel;
using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskManagementOsvin.Controllers
{
    public class ProfileController : ApiController
    {
        static readonly IProfile ProfileRepository = new ProfileRepository();

        [Route("~/api/Profile/GetProfilesByProjectId/{ProjectId}")]
        public HttpResponseMessage GetProfilesByProjectId(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listProfiles = ProfileRepository.GetProfiles(ProjectId);
                if (listProfiles == null)
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listProfiles);
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

        [Route("~/api/Profile/AddProfile")]
        public HttpResponseMessage AddUpdateProfile(AddUpworkProfileDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = ProfileRepository.AddProfile(model);
                if (response == null)
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else if(!response.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.NotImplemented, response);
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

        [Route("~/api/Profile/DeleteProfiles/{ProfileIds}")]
        [HttpPost]
        public HttpResponseMessage DeleteProfiles(string ProfileIds)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var deletedProfiles = ProfileRepository.DeleteProfiles(ProfileIds);
                if (deletedProfiles == null)
                {
                    httpResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, deletedProfiles);
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
