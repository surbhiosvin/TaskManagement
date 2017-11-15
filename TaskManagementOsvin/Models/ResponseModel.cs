using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class ResponseModel
    {
        public string response { get; set; } = "Failure";
        public bool isSuccess { get; set; }
    }
}