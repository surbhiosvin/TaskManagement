using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class UserDetailsModel : ResponseModel
    {
        public long UserId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfJoining { get; set; }
        public string FatherName { get; set; }
        public string SpouseName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string PersonalEmailId { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public roleTypeModel roleType { get; set; }
    }
    public enum roleTypeModel
    {
        Admin = 1,
        HR = 2,
        ProjectManager = 3,
        TeamLeader = 4,
        Employee = 5,
        NetworkAdmin=6
    }
}