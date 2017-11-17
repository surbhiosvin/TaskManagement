using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class UserListSearchModel
    {
        public long UserId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Archived { get; set; }
    }
}