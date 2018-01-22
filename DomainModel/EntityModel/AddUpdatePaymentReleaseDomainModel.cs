using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class AddUpdatePaymentReleaseDomainModel
    {
        public long PaymentId { get; set; }
        public long ProjectId { get; set; }
        public decimal ReleasedAmount { get; set; }
        public string NextDueDate { get; set; }
        public long CreatedBy { get; set; }
    }
}
