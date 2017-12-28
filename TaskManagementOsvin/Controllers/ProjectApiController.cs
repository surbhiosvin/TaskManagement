using DomainModel.EntityModel;
using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;

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

        [Route("~/api/Project/GetProjectDetailsById")]
        public HttpResponseMessage GetProjectDetailsById(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var ProjectType = ProjectRepository.GetProjectDetailsById(ProjectId);
                if (ProjectType == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    if (!string.IsNullOrEmpty(ProjectType.StartDate))
                    {
                        ProjectType.StartDate = DateTime.ParseExact(ProjectType.StartDate, "yyyy/MM/dd", null).ToString("dd/MM/yyyy");
                    }
                    if (!string.IsNullOrEmpty(ProjectType.EndDate))
                    {
                        ProjectType.EndDate = DateTime.ParseExact(ProjectType.EndDate, "yyyy/MM/dd", null).ToString("dd/MM/yyyy");
                    }
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

        [Route("~/api/Project/UpdateProjectStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateProjectStatus(UpdateProjectStatusDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = ProjectRepository.UpdateProjectStatus(model);
                if (response == null || response.isSuccess == false)
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

        [Route("~/api/Project/GetDepartmentAndEmployeeInProject/{ProjectId}")]
        public HttpResponseMessage GetDepartmentAndEmpInProject(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var result = ProjectRepository.GetDepartmentAndEmpInProject(ProjectId);
                if (result == null)
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

        [Route("~/api/Project/EmployeesWorkedOnProject/{ProjectId}")]
        [HttpGet]
        public HttpResponseMessage EmployeesWorkedOnProject(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var result = ProjectRepository.EmployeesWorkedOnProject(ProjectId);
                if (result == null)
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

        [Route("~/api/Project/WeeksBetweenDates/{ProjectId}")]
        [HttpGet]
        public HttpResponseMessage WeeksBetweenDates(long ProjectId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var result = ProjectRepository.WeeksBetweenDates(ProjectId);
                if (result == null)
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

        [Route("~/api/Project/GetHoursBetweenTwoDates")]
        [HttpPost]
        public HttpResponseMessage GetHoursBetweenTwoDates(GetHrsByDateAndProjDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                model.startdate = DateTime.ParseExact(model.startdate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                model.enddate = DateTime.ParseExact(model.enddate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                var result = ProjectRepository.GetHoursBetweenTwoDates(model);
                if (result == null)
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

        [Route("~/api/Project/GetIndividualWorkingHours")]
        [HttpPost]
        public HttpResponseMessage GetIndividualWorkingHours(GetIndividualWorkingHoursDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var result = ProjectRepository.GetIndividualWorkingHours(model);
                if (result == null)
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
                    var sd = DateTime.ParseExact(model.StartDate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    model.StartDate = sd;
                }
                if (!string.IsNullOrEmpty(model.EndDate))
                {
                    var ed = DateTime.ParseExact(model.EndDate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    model.EndDate = ed;
                }
                var response = ProjectRepository.AddUpdateProject(model);
                if (response == null)
                {
                    response.isSuccess = false;
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else if (response.response == "Success")
                {
                    response.isSuccess = true;
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    response.isSuccess = false;
                    httpResponse = Request.CreateResponse(HttpStatusCode.NotImplemented, response);
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

        
        [Route("~/api/Project/GetProjectPaymentById/{PaymentId}")]
        public HttpResponseMessage GetProjectPayment(long PaymentId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var objRes = ProjectRepository.GetPaymentById(PaymentId);
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
        [Route("~/api/Project/AddPaymentRelease")]
        public HttpResponseMessage AddPaymentRelase(AddUpdatePaymentReleaseDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var objRes = ProjectRepository.AddPaymentRelease(model);
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
        [Route("~/api/Project/UpdatePaymentRelease")]
        public HttpResponseMessage UpdatePaymentRelease(AddUpdatePaymentReleaseDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var objRes = ProjectRepository.UpdatePaymentRelease(model);
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
    }
}