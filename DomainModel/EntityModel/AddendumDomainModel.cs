using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class AddendumDomainModel
    {
        public long TimeEstimationId { get; set; }
        public long ProjectId { get; set; }
        public string EstimateHours { get; set; }
        public string AssignHours { get; set; }
        public string Description { get; set; }
        public string UploadDocument { get; set; }
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
        public string NetworkSupprotHours { get; set; }
        public bool IsApprove { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
