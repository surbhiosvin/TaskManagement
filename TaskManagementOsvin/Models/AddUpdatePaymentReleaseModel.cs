using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class AddUpdatePaymentReleaseModel
    {
        public long? PaymentId { get; set; }
        
        [Required]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        [Display(Name = "Released Amount")]
        public decimal? ReleasedAmount { get; set; }
        [Required]
        [Display(Name = "Next Due Date")]
        public string NextDueDate { get; set; }
        public long CreatedBy { get; set; }
    }
}