using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{   
        public class ChangePasswordReqModel
        {
            public long UserId { get; set; }
            [Required(ErrorMessage = "This field is required.")]
            public string OldPassword { get; set; }
            [Required(ErrorMessage = "This field is required.")]
            public string NewPassword { get; set; }
            [Compare("NewPassword", ErrorMessage = "Password did not match")]
            public string ConfirmPassword { get; set; }
        }
}