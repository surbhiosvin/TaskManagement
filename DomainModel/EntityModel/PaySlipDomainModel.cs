using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.EntityModel
{
    public class PaySlipDomainModel:ResponseDomainModel
    {
        public PaySlipDomainModel()
        {
            listEmployees = new List<EmployeeDomainModel>();
            listDepartments = new List<DepartmentDomainModel>();
            listDesignations = new List<DesignationDomainModel>();
        }
        public long PaySlipID { get; set; }
        public long EmployeeID { get; set; }

        public string EmployeeCode { get; set; }

        public int DepartmentId { get; set; }

        public int DesignationId { get; set; }

        public string DepartmentName { get; set; }

        public string DesignationName { get; set; }

        public string BankAccountNumber { get; set; }

        public string PanCardNumber { get; set; }
        public string PaySlipMonth { get; set; }

        public string DateOfJoining { get; set; }

        public string NumberOfDaysInCurrentMonth { get; set; }

        public string NumberOfDaysWorked { get; set; }

        public string BasicSalary { get; set; }

        public string MedicalAllowance { get; set; }

        public string OtherAllowance { get; set; }

        public string AmountForLeaveDeduction { get; set; }

        public string GrossSalary { get; set; }

        public string HouseRentAllowance { get; set; }

        public string ConveyanceAllowance { get; set; }

        public string TDS { get; set; }

        public string TakeHomeSalary { get; set; }

        public string EmployeeName { get; set; }

        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public List<EmployeeDomainModel> listEmployees { get; set; }
        public List<DesignationDomainModel> listDesignations { get; set; }
        public List<DepartmentDomainModel> listDepartments { get; set; }
    }
}
