using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class AddUpdateProjectModel
    {
        //public AddUpdateProjectModel()
        //{
        //    clients = new List<ClientModel>();
        //    projectType = new List<ProjectTypeModel>();
        //    employees = new List<EmployeesModel>();
        //}
        public long ProjectId { get; set; }
        [Display(Name = "client")]
        [Required(ErrorMessage = "Select a client")]
        public long ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string TechnologyName { get; set; }
        [Display(Name = "Project Type")]
        [Required(ErrorMessage = "Project Type is required")]
        public int ProjectTypeId { get; set; }
        [Display(Name = "Project Title")]
        [Required(ErrorMessage = "Project Title is required")]
        public string ProjectTitle { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [Display(Name = "Upwork Profile(s)")]
        [Required(ErrorMessage = "Upwork Profile(s) is required")]
        public string upworkprofiles { get; set; }
        public DateTime ClosedDate { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectStatus { get; set; }
        public string Archived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long CreatedBy { get; set; }
        public HttpPostedFileBase UploadDetailsDocumentFile { get; set; }
        public HttpPostedFileBase UploadDocumentFile { get; set; }
        public string UploadDetailsDocument { get; set; }
        public string UploadDocument { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? HourlyRate { get; set; }
        public decimal? EstimateHours  { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? AssignedHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? developerCodinghours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? WebserviceHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? EstimateDesignHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? SEOHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? AnalysisHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? ClientCommunicationHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? SetUpHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? MaintainenceHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? NetworkSupportHours { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid")]
        public decimal? QualityAnalystHours { get; set; }
        //public List<ClientModel> clients { get; set; }
        //public List<ProjectTypeModel> projectType { get; set; }
        //public List<EmployeesModel> employees { get; set; }
    }
}