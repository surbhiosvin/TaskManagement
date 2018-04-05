using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementOsvin.Security;

namespace TaskManagementOsvin.Security
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(string roles)
        {
            this.allowedroles = roles.ToLower().Split(',');
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (UserManager.user != null)
            {
                var loggedInRole = UserManager.user.Role.ToLower().Trim();
                if (allowedroles.Contains(loggedInRole))
                {
                    authorize = true;
                }
            }           
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
            filterContext.HttpContext.Response.Redirect("/Error/NoPermissions");
        }
    }
}