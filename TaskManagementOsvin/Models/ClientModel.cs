using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public partial class ClientModel
    {
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string TimeZone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string Skype { get; set; }
        public string Whatsapp { get; set; }
        public string PhoneNumber { get; set; }
        public string Archived { get; set; }
    }

    public partial class ClientModel
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}