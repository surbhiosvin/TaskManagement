using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class GetHrsByDateAndProjDomainModel
    {
        public long Projectid { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
}
