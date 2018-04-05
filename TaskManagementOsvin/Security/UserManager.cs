using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using TaskManagementOsvin.Models;
using System.Web.Mvc;
using TaskManagementOsvin.Controllers;
using System.Configuration;

namespace TaskManagementOsvin.Security
{
    public class UserManager
    {
        static string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
        public static UserDetailsModel user
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var a = HttpContext.Current.Session["CurrentUser"];
                    return HttpContext.Current.Session["CurrentUser"] as UserDetailsModel;
                }
                return (HttpContext.Current.User as CustomPrincipal).user;
            }
        }

        public static HttpStatusCode validateuser(User model)
        {
            try
            {
                var serialized = new JavaScriptSerializer().Serialize(model);
                var client = new HttpClient();
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                var result = client.PostAsync(BaseURL + "/api/Employee/AuthenticateUser", content).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var contents = result.Content.ReadAsStringAsync().Result;
                    var getUser = new JavaScriptSerializer().Deserialize<UserDetailsModel>(contents);
                    HttpContext.Current.Session["CurrentUser"] = getUser;
                    if (getUser != null)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string userData = serializer.Serialize(getUser);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                 1,
                                 model.email,
                                 DateTime.Now,
                                 DateTime.Now.AddMinutes(30),
                                 false,
                                 userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        HttpContext.Current.Response.Cookies.Add(faCookie);
                    }
                }
                return result.StatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie1);
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}