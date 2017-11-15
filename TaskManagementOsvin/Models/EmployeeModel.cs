using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class EmployeeModel
    {
        public int UserId { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
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
        public string LastCompanyDesignation { get; set; }
        public string DateOfJoiningInFirstCompany { get; set; }
        public string Qualification { get; set; }
        public string FileNo { get; set; }
        public string DateOfAnniversary { get; set; }
        public string DateOfLeaving { get; set; }
        public string ReasonOfLeaving { get; set; }
        public string Image { get; set; }
        public string IDCardFirst { get; set; }
        public string IDCardSecond_AddressProof_ { get; set; }

    }
}