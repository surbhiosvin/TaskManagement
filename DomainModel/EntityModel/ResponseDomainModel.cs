using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class ResponseDomainModel
    {
        public string response { get; set; } = "Failure";
        public bool isSuccess { get; set; }
    }
}
