using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Providers.Repositories;
using Providers.Providers.SP.Repositories;

namespace TaskManagementOsvin
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
    }
}