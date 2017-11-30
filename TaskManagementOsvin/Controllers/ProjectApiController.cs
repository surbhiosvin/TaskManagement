using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainModel.EntityModel;

namespace TaskManagementOsvin.Controllers
{
    public class ProjectApiController : ApiController
    {
        static readonly IProject ProjectRepository = new ProjectRepository();
        // GET api/<controller>
        [Route("~/api/Project/GetProjectType")]
        public HttpResponseMessage GetProjectType()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var ProjectType = ProjectRepository.GetProjectType();
                if (ProjectType == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, ProjectType);
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

        [Route("~/api/Project/GetProjectsByStatus")]
        [HttpPost]
        public HttpResponseMessage GetProjects(GetAllProjectsDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var Projects = ProjectRepository.GetAllProjectsBasedOnStatus(model);
                if (Projects == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, Projects);
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

        [Route("~/api/Project/UpdateProjectArchive")]
        [HttpPost]
        public HttpResponseMessage UpdateProjectArchive(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var result = ProjectRepository.UpdateProjectArchive(ProjectId);
                if (result == false)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, result);
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

        [Route("~/api/Project/GetProjectFullDetails")]
        public HttpResponseMessage GetProjectFullDetails(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var ProjectType = ProjectRepository.GetProjectFullDetails(ProjectId);
                if (ProjectType == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, ProjectType);
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
        // GET api/<controller>
        [Route("~/api/Project/GetProjectAddendumDetails")]
        public HttpResponseMessage GetProjectAddendumDetails(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listAddendums = ProjectRepository.GetProjectAddendumDetails(ProjectId);
                if (listAddendums == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listAddendums);
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

        [Route("~/api/Project/AddUpdateProject")]
        [HttpPost]
        public HttpResponseMessage AddUpdateProject(AddUpdateProjectDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                if (model.HourlyRate == null)
                {
                    model.HourlyRate = 0;
                }
                if (!string.IsNullOrEmpty(model.StartDate))
                {
                    model.StartDate = Convert.ToDateTime(model.StartDate).ToString("yyyy/MM/dd");
                }
                if (!string.IsNullOrEmpty(model.EndDate))
                {
                    model.EndDate = Convert.ToDateTime(model.EndDate).ToString("yyyy/MM/dd");
                }
                var respnose = ProjectRepository.AddUpdateProject(model);
                if (respnose == null)
                {
                    respnose.isSuccess = false;
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else if (respnose.response == "Success")
                {
                    respnose.isSuccess = true;
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, respnose);
                }
                else
                {
                    respnose.isSuccess = false;
                    httpResponse = Request.CreateResponse(HttpStatusCode.NotImplemented, respnose);
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