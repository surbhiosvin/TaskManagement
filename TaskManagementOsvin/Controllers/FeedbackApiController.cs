using DomainModel.EntityModel;
using Providers.Helper;
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
    public class FeedbackApiController : ApiController
    {
        static readonly IFeedback feedbackRepository = new FeedbackRepository();
        // GET api/<controller>
        [HttpGet]
        [Route("~/api/Feedback/GetProjectFeedback")]
        public HttpResponseMessage GetProjectFeedback(long ProjectId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                List<FeedbackDomainModel> listProjectFeedback = new List<FeedbackDomainModel>();
                listProjectFeedback = feedbackRepository.GetProjectFeedback(ProjectId);
                if (listProjectFeedback == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listProjectFeedback);
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
        [Route("~/api/Feedback/GetFeedList")]
        public HttpResponseMessage GetFeedList()
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                List<FeedbackDomainModel> listFeedback = new List<FeedbackDomainModel>();
                listFeedback = feedbackRepository.GetFeedList();
                if (listFeedback == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listFeedback);
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
        [Route("~/api/Feedback/GetAllFeedtypes")]
        public HttpResponseMessage GetAllFeedtypes()
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                List<FeedTypeDomainModel> listFeedTypes = new List<FeedTypeDomainModel>();
                listFeedTypes = feedbackRepository.GetAllFeedTypes();
                if (listFeedTypes == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, listFeedTypes);
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
        [Route("~/api/Feedback/AddUpdateFeedback")]
        public HttpResponseMessage AddUpdateFeedback(FeedbackDomainModel model)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var res = feedbackRepository.AddUpdateFeedback(model);
                if (res != null && res.isSuccess)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, res);
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "An error occurred, please try again or contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet]
        [Route("~/api/Feedback/DeleteFeedback")]
        public HttpResponseMessage DeleteFeedback(long FeedbackId)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = feedbackRepository.DeleteFeedback(FeedbackId);
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
        [Route("~/api/Feedback/ActivateDeactivateFeedback")]
        public HttpResponseMessage ActivateDeactivateFeedback(long FeedbackId, bool IsActive)
        {
            try
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var objRes = feedbackRepository.ActivateDeactivateFeedback(FeedbackId, IsActive);
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