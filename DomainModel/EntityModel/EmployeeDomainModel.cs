using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class EmployeeDomainModel
    {
        public int UserId { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string SpouseName { get; set; }
        public string DateOfJoining { get; set; }
        public String Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string PersonalEmailId { get; set; }
        public string LastCompanySalary { get; set; }
        public string LastCompanyName { get; set; }
        public string LastCompanyDesigantion { get; set; }
        public string DateOfJoiningInFirstCompany { get; set; }
        public string Qualification { get; set; }
        public string FileNo { get; set; }
        public string DateOfAnniversary { get; set; }
        public string DateOfLeaving { get; set; }
        public string ReasonOfLeaving { get; set; }
        public string Image { get; set; }
        public string IDCardFirst { get; set; }
        public string IDCardSecond_AddressProof_ { get; set; }
        public string CV { get; set; }
        public string ExperienceCertificate { get; set; }
        public string AppointementLetter { get; set; }
        public string AggrementDocument { get; set; }
        public string OtherDocument { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string Archived { get; set; }

    }
}
