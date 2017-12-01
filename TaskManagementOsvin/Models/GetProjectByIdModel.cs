using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class GetProjectByIdModel
    {
        public long ProjectId { get; set; }
        public long ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string TechnologyName { get; set; }
        public int ProjectTypeId { get; set; }
        public string ProjectTitle { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string UploadDetailsDocument { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ClosedDate { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectStatus { get; set; }
        public string Archived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpworkProfile { get; set; }
        public string EstimateHours { get; set; }
        public string AssignHours { get; set; }
        public string Description { get; set; }
        public string UploadDocument { get; set; }
        public string DeveloperCodingHours { get; set; }
        public string WebServiceHours { get; set; }
        public string DesignHours { get; set; }
        public decimal? DesignHourlyRate { get; set; }
        public string QAHours { get; set; }
        public decimal? QAHourlyRate { get; set; }
        public string SEOHours { get; set; }
        public decimal? SEOHourlyRate { get; set; }
        public string AnalysisHours { get; set; }
        public string ClientCommunicationHours { get; set; }
        public string SetUpHours { get; set; }
        public string MaintainenceHours { get; set; }
        public string NetworkSupprotHours { get; set; }
        public decimal? HourlyRate { get; set; }
    }
}