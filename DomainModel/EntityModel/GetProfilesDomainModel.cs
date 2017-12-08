using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class GetProfilesDomainModel
    {
        public long ProfileId { get; set; }
        public string ProfileName { get; set; }
        public long ProjectId { get; set; }
        public int ProjectTypeId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime createdDate { get; set; }
        public long createdBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal LoggedHours { get; set; }
        public long UpdatedBy { get; set; }
    }
}
