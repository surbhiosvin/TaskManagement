﻿using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskManagementOsvin.Controllers
{
    public class ProjectApiController : ApiController
    {
        static readonly IProject ProjectRepository = new ProjectRepository();
        // GET api/<controller>
        [Route("~/api/Project/GetProjectType")]
        public HttpResponseMessage GetClients()
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
    }
}