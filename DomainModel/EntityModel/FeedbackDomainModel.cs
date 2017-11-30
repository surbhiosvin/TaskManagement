using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class FeedbackDomainModel
    {
      public long FeedbackId { get; set; }
      public long ProjectId { get; set; }
        public long UserId { get; set; }
        public long FeedTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectTitle { get; set; }
        public decimal Feed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
