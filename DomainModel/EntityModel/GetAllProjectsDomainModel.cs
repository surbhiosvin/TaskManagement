using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class GetAllProjectsDomainModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string projecttitle { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string projectstatusMode { get; set; }
        public string projecttype { get; set; }
    }
}
