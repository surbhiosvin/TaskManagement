using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class AddUpdateProjectDomainModel
    {
        public long? ProjectId { get; set; }
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
        public DateTime ClosedDate { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectStatus { get; set; }
        public string Archived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long CreatedBy { get; set; }
        public string UploadDocument { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? EstimateHours { get; set; }
        public decimal? AssignedHours { get; set; }
        public decimal? developerCodinghours { get; set; }
        public decimal? WebserviceHours { get; set; }
        public decimal? EstimateDesignHours { get; set; }
        public decimal? SEOHours { get; set; }
        public decimal? AnalysisHours { get; set; }
        public decimal? ClientCommunicationHours { get; set; }
        public decimal? SetUpHours { get; set; }
        public decimal? MaintainenceHours { get; set; }
        public decimal? NetworkSupportHours { get; set; }
        public decimal? QualityAnalystHours { get; set; }
        public decimal? FixedPrice { get; set; }
    }
}
