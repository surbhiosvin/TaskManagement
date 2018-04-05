using Providers.Providers.SP.Repositories;
using Providers.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskManagementOsvin.Controllers
{
    public class NotificationController : ApiController
    {
        static readonly INotification NotificationRepository = new NotificationRepository();

        [Route("~/api/Notification/ProfileNotifications")]
        [HttpGet]
        public HttpResponseMessage ProfileNotifications()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var Notifications = NotificationRepository.ProfileToCompleteNotifications();
                if (Notifications == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, Notifications);
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

        [Route("~/api/Notification/BirthDayNotifications")]
        [HttpGet]
        public HttpResponseMessage BirthDayNotifications()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var Notifications = NotificationRepository.BirthdayNotifications();
                if (Notifications == null)
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Error Occurred");
                }
                else
                {
                    httpResponse = Request.CreateResponse(HttpStatusCode.OK, Notifications);
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
