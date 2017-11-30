using DomainModel.EntityModel;
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