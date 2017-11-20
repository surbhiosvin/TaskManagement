using DomainModel.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementOsvin.Models
{
    public class EmployeeModel
    {
        public int UserId { get; set; }
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "This field is required.")]
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "This field is required.")]
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }
        [Display(Name = "Date Of Joining")]
        public string DateOfJoining { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "This field is required.")]
        public string DOB { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Role { get; set; }
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        [Display(Name = "Current Address")]
        public string CurrentAddress { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Alternate Phone Number")]
        public string AlternatePhoneNumber { get; set; }
        [Display(Name = "Personal EmailId")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string PersonalEmailId { get; set; }
        [Display(Name = "Last Company Salary")]
        public string LastCompanySalary { get; set; }
        [Display(Name = "Last Company Name")]
        public string LastCompanyName { get; set; }
        [Display(Name = "Last Company Designation")]
        public string LastCompanyDesigantion { get; set; }
        [Display(Name = "Date Of Joining in First Company")]
        public string DateOfJoiningInFirstCompany { get; set; }
        public string Qualification { get; set; }
        [Display(Name = "File No.")]
        public string FileNo { get; set; }
        [Display(Name = "Date Of Anniversary")]
        public string DateOfAnniversary { get; set; }
        [Display(Name = "Date Of Leaving")]
        public string DateOfLeaving { get; set; }
        [Display(Name = "Reason Of Leaving")]
        public string ReasonOfLeaving { get; set; }
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string IDCardFirst { get; set; }
        [Display(Name = "Ist ID Proof :")]
        public HttpPostedFileBase IDCardFirst_File { get; set; }
        public string IDCardSecond_AddressProof_ { get; set; }
        [Display(Name = "IInd ID Proof (Addess Proof) :")]
        public HttpPostedFileBase IDCardSecond_AddressProof_File { get; set; }
        public string CV { get; set; }
        [Display(Name = "CV")]
        public HttpPostedFileBase CV_File { get; set; }
        public string ExperienceCertificate { get; set; }
        [Display(Name = "Experience Certificate")]
        public HttpPostedFileBase ExperienceCertificate_File { get; set; }
        public string AppointementLetter { get; set; }
        [Display(Name = "Appointement Letter")]
        public HttpPostedFileBase AppointementLetter_File { get; set; }
        public string AggrementDocument { get; set; }
        [Display(Name = "Aggrement Document")]
        public HttpPostedFileBase AggrementDocument_File { get; set; }
        public string OtherDocument { get; set; }
        [Display(Name = "Other Document")]
        public HttpPostedFileBase OtherDocument_File { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string Archived { get; set; }
        public List<DepartmentDomainModel> listDepartment { get; set; }
        public List<DesignationDomainModel> listDesignation { get; set; }

    }
}