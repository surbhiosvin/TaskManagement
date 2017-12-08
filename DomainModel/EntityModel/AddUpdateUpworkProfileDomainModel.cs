using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class AddUpworkProfileDomainModel
    {
        public string ProfileName { get; set; }
        public long ProjectId { get; set; }
        public int ProjectTypeId { get; set; }
        public decimal? hourlyRate { get; set; }
        public decimal loggedHours { get; set; }
        public decimal amount { get; set; }
        public long createdBy { get; set; }
    }
}
