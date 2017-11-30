using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ProjectWorkingHoursBeforePMSDomainModel
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string WorkinghHours { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Updateddate { get; set; }
        public string CreatedBy { get; set; }
    }
}
