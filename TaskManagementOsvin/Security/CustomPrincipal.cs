using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TaskManagementOsvin.Models;

namespace TaskManagementOsvin.Security
{
    public class CustomPrincipal: IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }
        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public UserDetailsModel user { get; set; }
    }
}