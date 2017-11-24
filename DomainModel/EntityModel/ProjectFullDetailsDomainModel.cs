using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ProjectFullDetailsDomainModel
    {
        public long ProjectId { get; set; }

        public long TimeEstimationId { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectUrl { get; set; }

        public long DepartmentId { get; set; }

        public int TypeId { get; set; }

        public string ProjectType { get; set; }

        public string DepartmentName { get; set; }

        public string TechnologyName { get; set; }

        public string ClientName { get; set; }

        public string FullDescription { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string ProjectStatus { get; set; }

        public string ClosedDate { get; set; }

        public string EstimateHours { get; set; }

        public string AssignHours { get; set; }

        public string Description { get; set; }

        public string IsApproved { get; set; }

        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public string TotalWorkingHours { get; set; }

        public int TotalEstimateHours { get; set; }

        public int TotalAssignHours { get; set; }

        //public decimal TotalworkingHours { get; set; }

        public decimal TotalWorkingHoursOfUser { get; set; }

        public Int64 ClientId { get; set; }

        public string UploadDocument { get; set; }

        public string UploadDetailsDocument { get; set; }

        public string DeveloperCodingHours { get; set; }

        public string WebServiceHours { get; set; }

        public string DesignHours { get; set; }

        public decimal DesignHourlyRate { get; set; }

        public string QAHours { get; set; }

        public decimal QAHourlyRate { get; set; }

        public string SEOHours { get; set; }

        public decimal SEOHourlyRate { get; set; }

        public string AnalysisHours { get; set; }

        public string ClientCommunicationHours { get; set; }

        public string SetUpHours { get; set; }

        public string MaintainenceHours { get; set; }

        public string NetworkSupportHours { get; set; }

        public decimal HourlyRate { get; set; }
    
        public string Startdate1 { get; set; }

        public string Enddate1 { get; set; }

        public string ArchiveMode { get; set; }
    }
}
