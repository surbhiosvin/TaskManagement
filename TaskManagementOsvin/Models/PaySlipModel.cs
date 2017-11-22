using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DomainModel.EntityModel;

namespace TaskManagementOsvin.Models
{
    public class PaySlipModel:ResponseModel
    {
        public PaySlipModel()
        {
            listEmployees = new List<EmployeeDomainModel>();
            listDepartments = new List<DepartmentDomainModel>();
            listDesignations = new List<DesignationDomainModel>();
        }
        public long PaySlipID { get; set; }
        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "This field is required.")]
        public long EmployeeID { get; set; }
        [Display(Name = "Employee Code")]
        [Required(ErrorMessage = "This field is required.")]
        public string EmployeeCode { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "This field is required.")]
        public long DepartmentId { get; set; }
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "This field is required.")]
        public long DesignationId { get; set; }      
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        [Display(Name = "Bank Account Number")]
        [Required(ErrorMessage = "This field is required.")]
        public string BankAccountNumber { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string PanCardNumber { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string PaySlipMonth { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string DateOfJoining { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string NumberOfDaysInCurrentMonth { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string NumberOfDaysWorked { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string BasicSalary { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string MedicalAllowance { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string OtherAllowance { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string AmountForLeaveDeduction { get; set; }
        public string GrossSalary { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string HouseRentAllowance { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string ConveyanceAllowance { get; set; }
      
        public string TDS { get; set; }
        public string TakeHomeSalary { get; set; }      
        public string EmployeeName { get; set; }       
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public string PaySlipMonthName { get; set; }
        public string PaySlipYear { get; set; }
        public List<EmployeeDomainModel> listEmployees { get; set; }
        public List<DesignationDomainModel> listDesignations { get; set; }
        public List<DepartmentDomainModel> listDepartments { get; set; }

    }
}