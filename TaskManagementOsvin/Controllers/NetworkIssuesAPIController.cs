using DomainModel.EntityModel;
using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Controllers
{
    public class NetworkIssuesAPIController : ApiController
    {
        INetwork networkRepository = new NetworkRepository();
        [HttpPost]
        [Route("~/api/NetworkIssues/GetReportIssues")]
        public HttpResponseMessage GetReportIssues(ReportIssueDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listReportIssues = networkRepository.GetReportIssues(model);
                if (listReportIssues == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listReportIssues);
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
        [Route("~/api/NetworkIssues/AddUpdateReportIssue")]
        public HttpResponseMessage AddUpdateReportIssue(ReportIssueDomainModel model)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var response = networkRepository.AddUpdateReportIssue(model);
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

        [HttpGet]
        [Route("~/api/NetworkIssues/GetIssueDescription")]
        public HttpResponseMessage GetIssueDescription(long ReportId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string Role = UserManager.user.Role;
                var IssueDescription = networkRepository.GetIssueDescription(ReportId, Role);
                if (IssueDescription == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, IssueDescription);
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

        [Route("~/api/NetworkIssues/GetReportedIssueCount")]
        public HttpResponseMessage GetReportedIssueCount(long UserId, string Role)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var listReportIssues = networkRepository.GetReportedIssueCount(UserId, Role);
                if (listReportIssues == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listReportIssues);
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
        [Route("~/api/NetworkIssues/ReportedIssueFeedBack")]
        public HttpResponseMessage ReportedIssueFeedBack(long ReportId, string RatingString)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                if (ReportId > 0)
                {
                    string[] str = RatingString.Split(',');
                    string SpeedOfSolution = str[0];
                    string Communication = str[1];
                    string Quality = str[2];
                    string Availability = str[3];
                    ReportIssueDomainModel model = new ReportIssueDomainModel() { SpeedOfSolution = SpeedOfSolution, Communication = Communication, Quality = Quality, Availability = Availability, ReportId = ReportId };
                    var response = networkRepository.ReportedIssueFeedBack(model);
                    if (response == null)
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                    }
                    else
                    {
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK, response);
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
        [HttpGet]
        [Route("~/api/NetworkIssues/GetIssueFeedBack")]
        public HttpResponseMessage GetIssueFeedBack(long ReportId)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var IssueDescription = networkRepository.GetIssueFeedBack(ReportId);
                if (IssueDescription == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, IssueDescription);
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
