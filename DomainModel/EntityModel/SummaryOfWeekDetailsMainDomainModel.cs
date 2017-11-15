using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class SummaryOfWeekDetailsMainDomainModel
    {
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public decimal WorkingHours { get; set; }
        public string ClientName { get; set; }
    }
}
