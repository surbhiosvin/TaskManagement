using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Providers.Repositories;
using Providers.Providers.SP.Repositories;
using DomainModel.EntityModel;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    public class EmployeeDailyStatusReportApiController : ApiController
    {
        static readonly IEmployeeDailyStatusReport employeeDailyStatusReportRepository = new EmployeeDailyStatusReportRepository();
        [Route("~/api/EmployeeDailyStatusReport/GetProjectReportCategoryByDeptId")]
        public HttpResponseMessage GetProjectReportCategoryByDeptId(long DepartmentId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listEmployees = employeeDailyStatusReportRepository.GetProjectReportCategoryByDeptId(DepartmentId);
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
        [Route("~/api/EmployeeDailyStatusReport/GetDailyStatusReportDetailsOfCurrentDate")]
        public HttpResponseMessage GetDailyStatusReportDetailsOfCurrentDate(long EmployeeId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listEmployees = employeeDailyStatusReportRepository.GetDailyStatusReportDetailsOfCurrentDate(EmployeeId);
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
        [Route("~/api/EmployeeDailyStatusReport/GetTime")]
        public HttpResponseMessage GetTime()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string nowtime = DateTime.Now.ToString("HH:mm:ss");
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, nowtime);
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
        [Route("~/api/EmployeeDailyStatusReport/AddUpdateEmployeeDailyReport")]
        [HttpPost]
        public HttpResponseMessage AddUpdateEmployeeDailyReport(AddEmployeeDailyStatusReportDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            ResponseDomainModel response = new ResponseDomainModel();
            try
            {
                if (model.listEmployeeDailyStatusReport != null && model.listEmployeeDailyStatusReport.Count > 0)
                {
                    response = employeeDailyStatusReportRepository.AddUpdateEmployeeDailyReport(model);
                    if (response == null)
                    {
                        response.isSuccess = false;
                        httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                    }
                    else if (response.isSuccess == true)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.isSuccess = false;
                        httpResponse = Request.CreateResponse(HttpStatusCode.NotImplemented, response);
                    }
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
        [Route("~/api/EmployeeDailyStatusReport/GetEmployeeTotalWorkingHoursReport")]
        public HttpResponseMessage GetEmployeeTotalWorkingHoursReport(long DepartmentId, long EmployeeId, string StartDate, string EndDate)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listEmployeeTotalWorkingHoursReport = employeeDailyStatusReportRepository.GetEmployeeTotalWorkingHoursReport(DepartmentId, EmployeeId, StartDate, EndDate);
                if (listEmployeeTotalWorkingHoursReport == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listEmployeeTotalWorkingHoursReport);
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