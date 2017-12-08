using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class GetPaymentDomainModel
    {
        public long PaymentId { get; set; }
        public long ProjectId { get; set; }
        public string PaymentDueDate { get; set; }
        public decimal ReleasedAmount { get; set; }
        public long CreatedBy { get; set; }
        public DateTime updatedDate { get; set; }
        public long updatedBy { get; set; }
    }
}
