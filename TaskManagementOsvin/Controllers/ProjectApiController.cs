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
        [Route("~/api/Project/GetProjectAssignToUserWithTotalWorkingHours")]
        public HttpResponseMessage GetProjectAssignToUserWithTotalWorkingHours(long ProjectId=0, long DepartmentId=0, string Status="")
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var ProjectAssignToUser = ProjectRepository.GetProjectAssignToUserWithTotalWorkingHours(ProjectId, DepartmentId, Status);
                if (ProjectAssignToUser == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, ProjectAssignToUser);
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
        [Route("~/api/Project/GetProjectList")]
        public HttpResponseMessage GetProjectList()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listProjects = ProjectRepository.GetProjectList();
                if (listProjects == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listProjects);
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
        [Route("~/api/Project/AddProjectWorkingHoursBeforePms")]
        public HttpResponseMessage AddProjectWorkingHoursBeforePms(ProjectWorkingHoursBeforePMSDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var objRes = ProjectRepository.AddProjectWorkingHoursBeforePms(model);
                if (objRes == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
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
        [Route("~/api/Project/AddUpdateReportCategory")]
        public HttpResponseMessage AddUpdateReportCategory(ProjectReportCategoryDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var objRes = ProjectRepository.AddUpdateReportCategory(model);
                if (objRes == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
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
        [Route("~/api/Project/ActivateDeactivateProjectReportCategory")]
        public HttpResponseMessage ActivateDeactivateProjectReportCategory(long CategoryId, bool IsActive)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = ProjectRepository.ActivateDeactivateProjectReportCategory(CategoryId, IsActive);
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
        [Route("~/api/Project/DeleteProjectReportCategory")]
        public HttpResponseMessage DeleteProjectReportCategory(long CategoryId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = ProjectRepository.DeleteProjectReportCategory(CategoryId);
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
        [Route("~/api/Project/GetProjectReportCategories")]
        public HttpResponseMessage GetProjectReportCategories()
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                List<ProjectReportCategoryDomainModel> listProjectReportCategories = new List<ProjectReportCategoryDomainModel>();
                listProjectReportCategories = ProjectRepository.GetProjectReportCategories();
                if (listProjectReportCategories == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listProjectReportCategories);
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
        [Route("~/api/Project/GetProjectTypeById")]
        public HttpResponseMessage GetProjectTypeById(long ProjectId=0)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listProjects = ProjectRepository.GetProjectList();
                if (listProjects == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    int ProjectTypeId = Convert.ToInt32(listProjects.Where(p => p.ProjectId == ProjectId).Select(s => s.ProjectTypeId).FirstOrDefault());
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, ProjectTypeId);
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
        [Route("~/api/Project/AddProjectTimeEstimation")]
        public HttpResponseMessage AddProjectTimeEstimation(AddendumDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var objRes = ProjectRepository.AddProjectTimeEstimation(model);
                if (objRes == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, objRes);
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
        [Route("~/api/Project/MergeProject")]
        public HttpResponseMessage MergeProject(long projectmergeto, long projectmergefrom)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = ProjectRepository.MergeProject(projectmergeto, projectmergefrom);
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